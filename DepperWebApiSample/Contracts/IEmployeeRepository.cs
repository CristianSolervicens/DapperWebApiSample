using DapperWebApiSample.Entities;

namespace DapperWebApiSample.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<IEnumerable<Employee>> GetEmployeesByCompany(int companyId);
    }
}
