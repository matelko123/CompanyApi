using AutoMapper;
using CompanyApi.Entities;
using CompanyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<Employeer, EmployeerDto>();
            CreateMap<EmployeerDto, Employeer>();

            CreateMap<CreateCompanyDto, Company>();

        }
    }
}
