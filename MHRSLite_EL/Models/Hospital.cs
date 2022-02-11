using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    [Table("Hospitals")]
   public class Hospital:Base<int>
    {
        [Required]
        [StringLength(400,MinimumLength =2,ErrorMessage ="Hastane adı en az 2 en fazla 400 karakter olmalıdır!")]
        public string HospitalName { get; set; }

        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District HospitalDistrict { get; set; }

        public virtual List<HospitalClinic> HospitalClinics { get; set; }

    }
}
