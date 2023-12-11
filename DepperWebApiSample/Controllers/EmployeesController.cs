
using DapperWebApiSample.Entities;
using DapperWebApiSample.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace DapperWebApiSample.Controllers
{

    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeesController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        /// <summary>
        /// Lsita de todos los Empleados, independiente de la Empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
