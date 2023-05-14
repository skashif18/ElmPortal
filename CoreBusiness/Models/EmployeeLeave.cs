using CoreBusiness.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    public class EmployeeLeave:DeletableEntity
    {
        public int EmployeeId { get; set; }
        [Required]
        public string Reason { get; set; }
        public int StatusId { get; set; }
        public string Comment { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
