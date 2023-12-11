using AutoMapper;

namespace DapperWebApiSample.Profiles;

public class CompanyProfile: Profile
{
    public CompanyProfile()
    {
        CreateMap<Models.CompanyForCreationAndUpdateDto, Entities.Company>();
        //CreateMap<Entities.Company, Models.CompanyForCreationAndUpdateDto>();
        CreateMap<Entities.Company, Models.CompanyWithEmplyeesDto>();
        CreateMap<Models.CompanyWithEmplyeesDto, Entities.Company>();
        CreateMap<Entities.Company, Models.CompanyWithoutEmployeesDto>();
    }
}
