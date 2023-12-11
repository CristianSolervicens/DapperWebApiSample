using DapperWebApiSample.Entities;
using System.ComponentModel.DataAnnotations;

namespace DapperWebApiSample.Models;

public class CompanyWithEmplyeesDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }

    public List<Employee> Employees { get; set; } = new List<Employee>();
}