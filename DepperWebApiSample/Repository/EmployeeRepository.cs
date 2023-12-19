using Dapper;
using DapperConsoleSample.Context;
using DapperWebApiSample.Entities;
using DapperWebApiSample.Contracts;
using System.Data;
using AutoMapper;

namespace DapperWebApiSample.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(DapperContext context, IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
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


        public async Task<Employee> GetEmployee(int id)
        {
            var query = "SELECT emp.Id, emp.Name, emp.Age, emp.Position, emp.CompanyId, CompanyName = comp.Name " +
                "FROM Employees emp join Companies comp on emp.CompanyId = comp.Id WHERE emp.Id = @id;";
            
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return employee;
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

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            var query = @"INSERT INTO Employees (Name, Age, Position, CompanyId) VALUES (@Name, @Age, @Position, @CompanyId);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int32);
            parameters.Add("Position", employee.Position, DbType.String);
            parameters.Add("CompanyId", employee.CompanyId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                //var createdEmployee = new Employee();
                //createdEmployee = _mapper.Map<Employee>(employee);
                //createdEmployee.Id = id;
                var New_employee =await GetEmployee(id);

                return New_employee;
            }
        }

    }
}
