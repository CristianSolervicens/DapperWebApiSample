using Dapper;
using DapperConsoleSample.Context;
using DapperWebApiSample.Entities;
using DapperWebApiSample.Contracts;

namespace DapperWebApiSample.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT emp.Id, emp.Name, emp.Age, emp.Position, emp.CompanyId, CompanyName = comp.Name " +
                "FROM Employees emp join Companies comp on emp.CompanyId = comp.Id;";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompany(int companyId)
        {
            var query = $"SELECT * FROM Employees WHERE CompanyId = {companyId};";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }



    }
}
