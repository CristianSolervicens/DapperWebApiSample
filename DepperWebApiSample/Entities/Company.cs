using DapperWebApiSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWebApiSample.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Name { get; set; }

    [MaxLength(250)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    public List<EmployeeWithoutCompanyDto> Employees { get; set; } = new List<EmployeeWithoutCompanyDto>();

}