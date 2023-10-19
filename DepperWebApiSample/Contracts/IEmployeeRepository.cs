using DapperConsoleSample.Entities;

namespace DepperWebApiSample.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
    }
}
