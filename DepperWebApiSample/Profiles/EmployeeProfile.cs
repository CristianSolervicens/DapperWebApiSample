﻿using AutoMapper;

namespace DapperWebApiSample.Profiles;

public class EmployeeProfile: Profile
{
    public EmployeeProfile()
    {
        CreateMap<Entities.Employee, Models.EmployeeWithoutCompanyDto>();
    }
    
}

