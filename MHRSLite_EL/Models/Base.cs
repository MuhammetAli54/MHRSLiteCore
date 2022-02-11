using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHRSLite_EL.Models
{
    public class Base<T> : IBase
    {
        [Key]
        [Column(Order = 1)]
        public T GetT { get; set; }

        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        [Display(Name ="Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }
    }
}
