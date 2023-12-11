
using AutoMapper;
using DapperConsoleSample.Contracts;
using DapperWebApiSample.Entities;
using DapperWebApiSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApiSample.Controllers;


/// <summary>
/// Api Compañías
/// </summary>
[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepo;
    private readonly IMapper _mapper;
    
    public CompaniesController(ICompanyRepository companyRepo, IMapper mapper)
    {
        _companyRepo = companyRepo??
            throw new ArgumentNullException(nameof(companyRepo));
        _mapper = mapper??
            throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Retorna Todas las compañías.
    /// </summary>
    /// <returns>json</returns>
    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(_mapper.Map<IEnumerable<CompanyWithoutEmployeesDto>>(companies));
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Obtiene el detalle de una compañía por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>json</returns>
    [HttpGet("{id}", Name = "CompanyById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompany(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompany(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Crea una nueva Compañía
    /// </summary>
    /// <param name="company"></param>
    /// <returns>json</returns>
    /// <remarks>
    /// Retorna la Compañía creada (sin empleados) con el Id interno que le fue asignado.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCompany(CompanyForCreationAndUpdateDto company)
    {
        try
        {
            Company retCompany = new Entities.Company();
            retCompany = _mapper.Map<Company>(company);
            var createdCompany = await _companyRepo.CreateCompany(retCompany);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, _mapper.Map<CompanyWithoutEmployeesDto>(createdCompany));
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Actualiza la Compañía identificada por el ID, todos los demás valores pueden cambiar.
    /// </summary>
    /// <param name="company"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCompany(int id, CompanyForCreationAndUpdateDto company)
    {
        try
        {
            Company retCompany = new Entities.Company();
            retCompany = _mapper.Map<Company>(company);
            retCompany.Id = id;
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepo.UpdateCompany(retCompany);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Elimina la compañía identificada por su ID en la URL
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepo.DeleteCompany(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Busca las compañías filtrándo por el ID de un Usuario.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>json</returns>
    [HttpGet("ByEmployeeId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyForEmployee(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompanyByEmployeeId(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Obtiene la colección de todos los Empleados.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>json</returns>
    [HttpGet("{id}/employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyWithEmployees(int id)
    {
        try
        {
            var company = await _companyRepo.GetCompanyEmployeesMultipleResults(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }


    /// <summary>
    /// Obtiene todas las compañias y sus empleados.
    /// </summary>
    /// <returns>json</returns>
    [HttpGet("employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompaniesEmployeesMultipleMapping()
    {
        try
        {
            var company = await _companyRepo.GetCompaniesEmployeesMultipleMapping();
            return Ok(company);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

}
