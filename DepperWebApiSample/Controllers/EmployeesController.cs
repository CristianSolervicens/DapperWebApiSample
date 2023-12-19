
using DapperWebApiSample.Entities;
using DapperWebApiSample.Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DapperWebApiSample.Models;

namespace DapperWebApiSample.Controllers
{

    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo??
                throw new ArgumentNullException(nameof(employeeRepo));
            _mapper = mapper??
                throw new ArgumentNullException(nameof(mapper));
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

        /// <summary>
        /// Obtiene Empleado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee</returns>
        [HttpGet("{id}", Name = "EmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepo.GetEmployee(id);
                if (employee == null)
                    return NotFound();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Crea un Empleado
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeCreatedDto</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEmployee(EmployeeForCreationDto employee)
        {
            try
            {
                var retEmployee = new Entities.Employee();
                retEmployee = _mapper.Map<Employee>(employee);
                var createdCompany = await _employeeRepo.CreateEmployee(retEmployee);
                return CreatedAtRoute("EmployeeById", new { id = createdCompany.Id }, retEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
