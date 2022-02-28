using AutoMapper;
using MHRSLite_EL.Models;
using MHRSLite_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            //Appointment'ı AppointmentVM ye dönüştür
            CreateMap<Appointment, AppointmentVM>();
            //AppointmentVM yi Appointment a dönüştür.
            CreateMap<AppointmentVM, Appointment>();

            //ReverseMap sayesinde yukarıdaki 2 ayrı işlemi tek satırda yapmış oluyoruz.
            //Appointment AppointmentVM ye dönüşebilir.
            //AppointmentVM Appointmenta dönüşebilir.
            CreateMap<Appointment, Appointment>().ReverseMap();
        }
    }
}
