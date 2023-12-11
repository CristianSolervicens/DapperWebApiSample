using System.ComponentModel.DataAnnotations;

namespace DapperWebApiSample.Models;

public class CompanyForCreationAndUpdateDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
}
