using AutoMapper;
using CoreBusiness;
using CoreBusiness.Models;
using Portal.API.Features.Doctors.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using UseCases.DataStorePluginInterfaces.Generic;

namespace Portal.API.Features.Doctors.Service
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IGenericRepository<Employee> repository;
        private readonly IGenericRepository<EmployeeLeave> employeeLeave_repository;

        private readonly IMapper mapper;

        private Employee dbModel = new();
        private EmployeeLeave Appointment_dbModel = new();
        public EmployeeService(IGenericRepository<Employee> _repository,  IMapper mapper, IGenericRepository<EmployeeLeave> _appointment_repository)
        {
            repository = _repository;
            this.mapper = mapper;
            employeeLeave_repository = _appointment_repository;
        }
        public Response Create(EmployeeRequestModel model)
        {
            dbModel = mapper.Map<Employee>(model);
            return repository.Create(dbModel);
        }

        public Response CreateEmployeeLeave (EmployeeLeaveRequestModel model)
        {
            Appointment_dbModel = mapper.Map<EmployeeLeave>(model);
            return employeeLeave_repository.Create(Appointment_dbModel);
        }

        public Response Delete(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EmployeeResponseModel> GetAll()
        {
            var list = repository.GetAll();
            return mapper.Map<IList<EmployeeResponseModel>>(list);
        }

        public EmployeeLeaveResponseModel GetEmployeeLeave(int Id)
        {
            var model = employeeLeave_repository.GetById(Id);
            return mapper.Map<EmployeeLeaveResponseModel>(model);
        }

        public IEnumerable<EmployeeLeaveResponseModel> GetAppointments()
        {
            var list = employeeLeave_repository.GetAll();
            return mapper.Map<IList<EmployeeLeaveResponseModel>>(list);
        }

        public Response Update(EmployeeRequestModel model)
        {
            dbModel = mapper.Map<Employee>(model);
            return repository.Update(dbModel);
        }

        public IEnumerable<IdNameResponseModel> GetEmployees()
        {
            var list = repository.GetAll();
            return mapper.Map<IList<IdNameResponseModel>>(list);
        }

    }
}
