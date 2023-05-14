using CoreBusiness;
using CoreBusiness.Models;
using Portal.API.Features.Doctors.Models;
using System.Collections.Generic;

namespace Portal.API.Features.Doctors.Service
{
    public interface IEmployeeService
    {
        Response Create(EmployeeRequestModel model);
        Response Update(EmployeeRequestModel model);
        IEnumerable<EmployeeResponseModel> GetAll();
        Response Delete(int Id);
        IEnumerable<IdNameResponseModel> GetEmployees();

    }
}
