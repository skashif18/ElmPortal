using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.API.Features.Doctors.Models
{
    public class EmployeeLeaveRequestModel
    {
        [Key]
        public int Id { get; set; }


        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public string Mobile { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
