using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    [Table("Cities")]
    public class City : Base<byte>
    {
        [Required]
        [StringLength(50,MinimumLength =2,ErrorMessage ="İl adı en az 2 en çok 50 karakter olmalıdır!")]
        public string CityName { get; set; }

        [Required]
        public byte PlateCode { get; set; }
        //İlçeler ile ilgisi var.
        public virtual List<District> Districts { get; set; }
    }
}
