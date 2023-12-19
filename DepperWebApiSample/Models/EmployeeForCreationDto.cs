using System.ComponentModel.DataAnnotations;

namespace DapperWebApiSample.Models
{
    public class EmployeeForCreationDto
    {
        [MaxLength(300)]
        public string Name { get; set; }

        public int Age { get; set; }

        [MaxLength(250)]
        public string Position { get; set; }

        public int CompanyId { get; set; }
    }
}
