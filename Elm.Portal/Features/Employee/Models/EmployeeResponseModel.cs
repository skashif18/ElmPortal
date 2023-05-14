using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.API.Features.Doctors.Models
{
    public class EmployeeResponseModel
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Designation { get; set; }

        public string CreationUserName { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdateUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }


        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
