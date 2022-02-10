using MHRSLite_EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_BLL.EmailService
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
