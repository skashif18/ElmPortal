using CoreBusiness.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBusiness.Models
{
    public class Employee:DeletableEntity
    {
        public Employee()
        {
            EmployeeLeave = new HashSet<EmployeeLeave>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public string Designation { get; set; }
        public int? ManagerId { get; set; }
        public bool IsManager { get; set; } = false;

        public virtual ICollection<EmployeeLeave> EmployeeLeave { get; set; }
    }

}
