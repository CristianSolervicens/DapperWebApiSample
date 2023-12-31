﻿using AutoMapper;
using Dapper;
using DapperConsoleSample.Context;
using DapperConsoleSample.Contracts;
using DapperWebApiSample.Entities;
using DapperWebApiSample.Models;
using DapperWebApiSample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepperConsoleSample.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext _context;
    private readonly IMapper _mapper;

    public CompanyRepository(DapperContext context, IMapper mapper)
    {
        _context = context??
            throw new ArgumentNullException(nameof(context));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        var query = "SELECT * FROM Companies";

        using (var connection = _context.CreateConnection())
        {
            var companies = await connection.QueryAsync<Company>(query);
            return companies.ToList();
        }
    }

    public async Task<Company> GetCompany(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @Id";
        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
            return company;
        }
    }

    public async Task<Company> CreateCompany(Company company)
    {
        var query = @"INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country);
                SELECT CAST(SCOPE_IDENTITY() as int);";

        var parameters = new DynamicParameters();
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, parameters);
            var createdCompany = new Company();

            createdCompany = _mapper.Map<Company>(company);
            createdCompany.Id = id;
            return createdCompany;
        }
    }

    public async Task UpdateCompany(Company company)
    {
        var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", company.Id, DbType.Int32);
        parameters.Add("Name", company.Name, DbType.String);
        parameters.Add("Address", company.Address, DbType.String);
        parameters.Add("Country", company.Country, DbType.String);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteCompany(int id)
    {
        var query = "DELETE FROM Companies WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { id });
        }
    }

    public async Task<Company> GetCompanyByEmployeeId(int id)
    {
        var procedureName = "ShowCompanyForProvidedEmployeeId";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

        using (var connection = _context.CreateConnection())
        {
            var company = await connection.QueryFirstOrDefaultAsync<Company>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            return company;
        }
    }

    public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @Id;" +
                    "SELECT Id, Name, Age, Position FROM Employees WHERE CompanyId = @Id";

        using (var connection = _context.CreateConnection())
        using (var multi = await connection.QueryMultipleAsync(query, new { id }))
        {
            var company = await multi.ReadSingleOrDefaultAsync<Company>();
            if (company != null)
            {
                var employees = new List<Employee>();
                employees = (await multi.ReadAsync<Employee>()).ToList();

                company.Employees = _mapper.Map<List<EmployeeWithoutCompanyDto>>(employees);
            }
            return company;
        }
    }

    public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
    {
        var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId";

        using (var connection = _context.CreateConnection())
        {
            var companyDict = new Dictionary<int, Company>();

            var companies = await connection.QueryAsync<Company, Employee, Company>(
                query, (company, employee) =>
                {
                    if (!companyDict.TryGetValue(company.Id, out var currentCompany))
                    {
                        currentCompany = company;
                        companyDict.Add(currentCompany.Id, currentCompany);
                    }

                    currentCompany.Employees.Add(_mapper.Map<EmployeeWithoutCompanyDto>(employee));
                    return currentCompany;
                }
            );

            return companies.Distinct().ToList();
        }
    }

}
