﻿using MHRSLite_BLL.Contracts;
using MHRSLite_BLL.EmailService;
using MHRSLite_EL;
using MHRSLite_EL.Enums;
using MHRSLite_EL.IdentityModels;
using MHRSLite_EL.Models;
using MHRSLite_UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MHRSLite_UI.Areas.Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        //Dependency Injection

        public DoctorController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEmailSender emailSender, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var checkUserForUserName = await _userManager.FindByNameAsync(model.TCNumber);
                if (checkUserForUserName != null)
                {
                    ModelState.AddModelError(nameof(model.TCNumber), "Bu TC kimlik numarası ile sisteme daha önce kayıt olunmuştur!");
                    return View(model);
                }
                var checkUserForEmail = await _userManager.FindByEmailAsync(model.Email);
                if (checkUserForEmail != null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Bu email ile sisteme daha önce kayıt olunmuştur.");
                    return View(model);
                }
                //Yeni kullanıcı oluşturalım.
                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    UserName = model.TCNumber,
                    Gender = model.Gender
                    //TO DO : Birthdate?
                    //TO DO : PhoneNumber
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var roleResult = _userManager.AddToRoleAsync(newUser, RoleNames.PassiveDoctor.ToString());
                    //email gönderilmelidir.
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = code }, protocol: Request.Scheme);

                    var emailMessage = new EmailMessage()
                    {
                        Contacts = new string[] { newUser.Email },
                        Subject = "MHRSLITE - Email Aktivasyonu",
                        Body = $"Merhaba Dr. {newUser.Name} {newUser.Surname},<br> Hesabınızı aktifleştirmek için <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>buraya<a/> tıklayınız."
                    };
                    await _emailSender.SendAsync(emailMessage);
                    //doctor tablosuna ekleme yapılmalıdır.
                    Doctor newDoctor = new Doctor()
                    {
                        TCNumber = model.TCNumber,
                        UserId = newUser.Id
                    };
                    if (_unitOfWork.DoctorRepository.Add(newDoctor) == false)
                    {
                        //sistem yöneticisine email gitsin.
                        var emailMessageToAdmin = new EmailMessage()
                        {
                            Contacts = new string[] { _configuration.GetSection("ManagerEmails:Email").Value },
                            CC = new string[] { _configuration.GetSection("ManagerEmails:EmailToCC").Value },
                            Subject = "MHRSLITE - HATA! Doktor Tablosu",
                            Body = $"Aşağıdaki bilgilere sahip kişi eklenirken hata olmuş.Doktor tablosuna bilgileri ekleyiniz. <br/> Bilgiler: TCNumber:{model.TCNumber} <br/> UserId{newUser.Id}"
                        };
                        await _emailSender.SendAsync(emailMessageToAdmin);
                    }
                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                return View();
            }
        }
    }
}
