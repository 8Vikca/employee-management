using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Interfaces.Services;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("/getActiveEmployees")]
        public List<EmployeeViewModel> GetActiveEmployees()
        {
            return _employeeService.GetActiveEmployees();
        }

        [HttpGet("/getInactiveEmployees")]
        public List<EmployeeViewModel> GetInactiveEmployees()
        {
            return _employeeService.GetInactiveEmployees();
        }

        [HttpPost("/createEmployee")]
        public IActionResult CreateEmployee([FromBody] NewEmployeeViewModel newEmployeeViewModel)
        {
            if (_employeeService.CreateEmployee(newEmployeeViewModel))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("/editEmployee")]
        public IActionResult EditEmployee([FromBody] EditEmployeeViewModel editEmployeeViewModel)           
        {
            if (_employeeService.EditEmployee(editEmployeeViewModel))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("/deleteEmployee")]
        public IActionResult DeleteEmployee(int emplyeeId)
        {
            if (!_employeeService.DeleteEmployee(emplyeeId))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
