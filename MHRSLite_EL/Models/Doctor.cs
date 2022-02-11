using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    [Table("Doctors")]
    public class Doctor : Base<string>
    {
        public virtual List<HospitalsClinics> HospitalClinics { get; set; }
    }
}
