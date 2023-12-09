
using DapperConsoleSample.Contracts;
using DapperConsoleSample.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DepperWebApiSample.Controllers;


/// <summary>
/// Api Compañías
/// </summary>
[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _companyRepo;
    
    public CompaniesController(ICompanyRepository companyRepo)
    {
        _companyRepo = companyRepo;
    }

    /// <summary>
    /// Retorna Todas las compañías.
    /// </summary>
    /// <returns>json</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(companies);
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
    /// No se requiere el ID, este es retornado en el response
    /// </summary>
    /// <param name="company"></param>
    /// <returns>json</returns>
    /// <remarks>
    /// Retorna el item creado con el Id interno que le fue asignado.
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCompany(Company company)
    {
        try
        {
            var createdCompany = await _companyRepo.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
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
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCompany(Company company)
    {
        try
        {
            var dbCompany = await _companyRepo.GetCompany(company.Id);
            if (dbCompany == null)
                return NotFound();

            await _companyRepo.UpdateCompany(company);
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
    [HttpGet("{id}/MultipleResult")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCompanyEmployeesMultipleResult(int id)
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
    [HttpGet("MultipleMapping")]
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
