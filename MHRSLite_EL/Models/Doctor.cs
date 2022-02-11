using MHRSLite_EL.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    [Table("Doctors")]
    public class Doctor : PersonBase
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; } // Identity Model'in ID değeri burada Foreign Key olacaktır.
        public virtual List<HospitalClinics> HospitalClinics { get; set; }
    }
}
