using System.ComponentModel.DataAnnotations;
using System;

namespace Portal.API.Features.Doctors.Models
{
    public class EmployeeLeaveResponseModel
    {
        [Key]
        public int Id { get; set; }


        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string Mobile { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string CreationUserName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string LastUpdateUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }


        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
