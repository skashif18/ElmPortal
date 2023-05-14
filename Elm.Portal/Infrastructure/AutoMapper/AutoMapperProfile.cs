using Appo.Server.Features.Identity.Models;
using AutoMapper;
using CoreBusiness.Models;
using Portal.API.Features.Doctors.Models;

namespace Appo.Server.Infrastructure.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeRequestModel, Employee>();//for post mapping
            CreateMap<Employee, EmployeeResponseModel>();//for get mapping

            CreateMap<EmployeeLeaveRequestModel, EmployeeLeave>();//for post mapping
            CreateMap<EmployeeLeave, EmployeeLeaveResponseModel>();//for get mapping

            CreateMap<Employee, IdNameResponseModel>();//for get mapping

        }
    }
}
