using CoreBusiness.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    public class Status : DeletableEntity
    {
        [Required]
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
