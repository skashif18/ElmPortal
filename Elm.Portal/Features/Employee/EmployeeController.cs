using Appo.Server.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.API.Features.Doctors.Models;
using Portal.API.Features.Doctors.Service;
using System.Collections.Generic;

namespace Portal.API.Features.Doctors
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService service;
        public EmployeeController(IEmployeeService _service)
        {
            service = _service;
        }

        [HttpGet]
        [Route("Employee")]
        public IEnumerable<EmployeeResponseModel> GetAll()
            => service.GetAll();

        [HttpPost]
        [Route("create")]
        public IActionResult Create(EmployeeRequestModel model)
        {
            var result = service.Create(model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpGet]
        [Route("Employee-dropdown")]
        [AllowAnonymous]
        public IEnumerable<IdNameResponseModel> GetDoctors()
          => service.GetEmployees();


        [HttpPut]
        [Route("update")]
        public IActionResult Update(EmployeeRequestModel model)
        {
            var result = service.Update(model);

            if (!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result);
        }

        
    }
}
