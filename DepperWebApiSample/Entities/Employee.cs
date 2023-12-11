using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWebApiSample.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }

        public int Age { get; set; }

        [MaxLength(250)]
        public string Position { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
