using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    public class Appointment : Base<int>
    {
        public string PatientId { get; set; }
        public int HospitalClinicId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(5,MinimumLength =5,ErrorMessage ="Randevu saati XX:XX şeklinde olmalıdır!")]
        public DateTime AppointmentHour { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("HospitalClinicId")]
        public virtual HospitalClinics HospitalClinic { get; set; }
    }
}
