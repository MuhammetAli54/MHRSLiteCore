using AutoMapper;
using MHRSLite_BLL.Contracts;
using MHRSLite_DAL;
using MHRSLite_EL.Constants;
using MHRSLite_EL.Enums;
using MHRSLite_EL.IdentityModels;
using MHRSLite_EL.Models;
using MHRSLite_EL.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_BLL.Implementations
{
    public class AppointmentRepository:Repository<Appointment>,IAppointmentRepository
    {
        //Global alan
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public AppointmentRepository(MyContext myContext
            ,IMapper mapper, UserManager<AppUser> userManager
            ):base(myContext)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public AppointmentVM GetAppointmentByID(string patientid, int hcid, DateTime appointmentDate, string appointmentHour)
        {
            var data = GetFirstOrDefault(x=>
            x.PatientId==patientid
            && x.HospitalClinicId==hcid
            && x.AppointmentDate==appointmentDate
            && x.AppointmentHour==appointmentHour,
            includeProperties:"HospitalClinic,Patient");
            if (data!=null)
            {
                //Hastane
                data.HospitalClinic.Hospital = _myContext.Hospitals.FirstOrDefault(x => x.Id == data.HospitalClinic.HospitalId);
                //Klinik
                data.HospitalClinic.Clinic = _myContext.Clinics.FirstOrDefault(x => x.Id == data.HospitalClinic.ClinicId);
                //İlçe
                data.HospitalClinic.Hospital.HospitalDistrict = _myContext.Districts.FirstOrDefault(x => x.Id == data.HospitalClinic.Hospital.DistrictId);
                //il
                data.HospitalClinic.Hospital.HospitalDistrict.City = _myContext.Cities.FirstOrDefault(x => x.Id == data.HospitalClinic.Hospital.HospitalDistrict.CityId);
                //Doktor
                data.HospitalClinic.Doctor = _myContext.Doctors.FirstOrDefault(x => x.TCNumber == data.HospitalClinic.DoctorId);
                //AppUser --> tcnumber username olarak appuserda kayıtlıdır
                data.HospitalClinic.Doctor.AppUser = _userManager.FindByNameAsync(data.HospitalClinic.DoctorId).Result;
                var returnData = _mapper.Map<Appointment, AppointmentVM>(data);
                return returnData;
            }
            return null;
        }

        public List<AppointmentVM> GetPastAppointments(string patientid)
        {
            try
            {
                var data = GetAll(x=> 
                x.PatientId==patientid
                && x.AppointmentStatus!=AppointmentStatus.Active,
                includeProperties:"HospitalClinic,Patient").ToList();

                foreach (var item in data)
                {
                    //Hastane
                    item.HospitalClinic.Hospital = _myContext.Hospitals.FirstOrDefault(x => x.Id == item.HospitalClinic.HospitalId);
                    //Klinik
                    item.HospitalClinic.Clinic = _myContext.Clinics.FirstOrDefault(x => x.Id == item.HospitalClinic.ClinicId);
                    //İlçe
                    item.HospitalClinic.Hospital.HospitalDistrict = _myContext.Districts.FirstOrDefault(x => x.Id == item.HospitalClinic.Hospital.DistrictId);
                    //il
                    item.HospitalClinic.Hospital.HospitalDistrict.City = _myContext.Cities.FirstOrDefault(x => x.Id == item.HospitalClinic.Hospital.HospitalDistrict.CityId);
                    //Doktor
                    item.HospitalClinic.Doctor = _myContext.Doctors.FirstOrDefault(x => x.TCNumber == item.HospitalClinic.DoctorId);
                    //AppUser --> tcnumber username olarak appuserda kayıtlıdır
                    item.HospitalClinic.Doctor.AppUser = _userManager.FindByNameAsync(item.HospitalClinic.DoctorId).Result;
                }
                var returnData = _mapper.Map<List<Appointment>, List<AppointmentVM>>(data);
                return returnData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppointmentVM> GetUpComingAppointments(string patientid)
        {
            try
            {
                var data = GetAll(x =>
                x.PatientId == patientid
                && x.AppointmentStatus == AppointmentStatus.Active,
                includeProperties: "HospitalClinic,Patient").ToList();

                foreach (var item in data)
                {
                    //Hastane
                    item.HospitalClinic.Hospital = _myContext.Hospitals.FirstOrDefault(x => x.Id == item.HospitalClinic.HospitalId);
                    //Klinik
                    item.HospitalClinic.Clinic = _myContext.Clinics.FirstOrDefault(x => x.Id == item.HospitalClinic.ClinicId);
                    //İlçe
                    item.HospitalClinic.Hospital.HospitalDistrict = _myContext.Districts.FirstOrDefault(x => x.Id == item.HospitalClinic.Hospital.DistrictId);
                    //il
                    item.HospitalClinic.Hospital.HospitalDistrict.City = _myContext.Cities.FirstOrDefault(x => x.Id == item.HospitalClinic.Hospital.HospitalDistrict.CityId);
                    //Doktor
                    item.HospitalClinic.Doctor = _myContext.Doctors.FirstOrDefault(x => x.TCNumber == item.HospitalClinic.DoctorId);
                    //AppUser --> tcnumber username olarak appuserda kayıtlıdır
                    item.HospitalClinic.Doctor.AppUser = _userManager.FindByNameAsync(item.HospitalClinic.DoctorId).Result;
                }
                var returnData = _mapper.Map<List<Appointment>, List<AppointmentVM>>(data);
                return returnData;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// verilen tarihten büyük olan iptal edilmemiş aktif yada geçmiş DAHİLİYE randevularını getirir.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<AppointmentVM> GetAppointmentsIM(DateTime? dt)
        {
            try
            {
                List<AppointmentVM> data = new List<AppointmentVM>();
                var result = from a in _myContext.Appointments join hcid in _myContext.HospitalClinics on a.HospitalClinicId equals hcid.Id join c in _myContext.Clinics on hcid.ClinicId equals c.Id where c.ClinicName == ClinicsConstants.INTERNAL_MEDICINE && a.AppointmentStatus == AppointmentStatus.Pass select a;
                if (dt!=null)
                    {
                    var date = Convert.ToDateTime(dt.Value.ToString("dd/MM/yyyy"));
                    result = result.Where(x=> x.AppointmentDate>=date);
                    }
                foreach (var item in result)
                {
                    item.Patient = _myContext.Patients.FirstOrDefault(x => x.TCNumber == item.PatientId);
                    //AppUser --> tcnumber username olarak appuserda kayıtlıdır
                    item.HospitalClinic.Doctor.AppUser = _userManager.FindByNameAsync(item.PatientId).Result;
                }
                data = _mapper.Map<List<Appointment>,List<AppointmentVM>>(result.ToList());
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
