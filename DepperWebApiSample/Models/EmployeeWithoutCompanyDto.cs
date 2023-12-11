using System.ComponentModel.DataAnnotations;

namespace DapperWebApiSample.Models
{
    public class EmployeeWithoutCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}
