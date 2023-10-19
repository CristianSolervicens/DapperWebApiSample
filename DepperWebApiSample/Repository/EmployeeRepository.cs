using Dapper;
using DapperConsoleSample.Context;
using DapperConsoleSample.Entities;
using DepperWebApiSample.Contracts;

namespace DepperWebApiSample.Repository
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
            var query = "SELECT * FROM Employees";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }
    }
}
