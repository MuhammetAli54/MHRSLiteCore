using MHRSLite_EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHRSLite_UI.Models
{
    public class AvailableDoctorAppointmentHoursViewModel
    {
        //dr adı
        //tarih --yarın
        //saat başları
        //saat detayları

        public Doctor Doctor { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string HourBase { get; set; }
        public List<string> Hours { get; set; } = new List<string>();
    }
}
