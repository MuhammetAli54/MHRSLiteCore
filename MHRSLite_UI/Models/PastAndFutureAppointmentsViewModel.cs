using MHRSLite_EL.IdentityModels;
using MHRSLite_EL.Models;
using MHRSLite_EL.PagingListModels;
using MHRSLite_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHRSLite_UI.Models
{
    public class PastAndFutureAppointmentsViewModel
    {
        //public List<Appointment> PastAppointments { get; set; }
        //public List<Appointment> UpcomingAppointments { get; set; }

        //Randevuları sayfalama yaparak görüntülemek istediğimiz için PaginatedListi kullandık
        public PaginatedList<AppointmentVM> PastAppointments { get; set; }
        public PaginatedList<AppointmentVM> UpcomingAppointments { get; set; }
    }
}
