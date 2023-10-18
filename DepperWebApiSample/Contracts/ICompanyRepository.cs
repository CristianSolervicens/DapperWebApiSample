using DapperConsoleSample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperConsoleSample.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(Company company);
        public Task UpdateCompany(Company company);
        public Task DeleteCompany(int id);
        public Task<Company> GetCompanyByEmployeeId(int id);
        public Task<Company> GetCompanyEmployeesMultipleResults(int id);
        public Task<List<Company>> GetCompaniesEmployeesMultipleMapping();
    }
}
