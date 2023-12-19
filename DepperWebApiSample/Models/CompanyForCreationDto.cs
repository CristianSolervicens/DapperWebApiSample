using System.ComponentModel.DataAnnotations;

namespace DapperWebApiSample.Models;

public class CompanyForCreationAndUpdateDto
{
    [Required]
    [MaxLength(150)]
    public string? Name { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }
}
