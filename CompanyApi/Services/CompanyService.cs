using AutoMapper;
using CompanyApi.Entities;
using CompanyApi.Exceptions;
using CompanyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApi.Services
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAll();
        CompanyDto GetById(long id);
        long Create(CreateCompanyDto dto);
        void Update(long id, CreateCompanyDto dto);
        void Delete(long id);
        IEnumerable<CompanyDto> Search(SearchCompanyDto dto);
    }

    public class CompanyService : ICompanyService
    {
        private readonly CompanyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyService(CompanyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            var companies = _dbContext
                .Companies
                .Include(c => c.Employees)
                .ToList();

            var result = _mapper.Map<List<CompanyDto>>(companies);
            return result;
        }

        public CompanyDto GetById(long id)
        {
            var company = _dbContext
                .Companies
                .Include(c => c.Employees)
                .FirstOrDefault(r => r.Id == id);

            if(company is null)
                throw new NotFoundException("Company not found");

            var result = _mapper.Map<CompanyDto>(company);
            return result;
        }

        public long Create(CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();

            return company.Id;
        }

        public void Update(long id, CreateCompanyDto dto)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);

            if(company is null)
                throw new NotFoundException("Company not found");

            company.Name = dto.Name;
            company.EstablishmentYear = dto.EstablishmentYear;
            company.Employees = _mapper.Map<List<Employeer>>(dto.Employees);

            _dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);

            if(company is null)
                throw new NotFoundException("Company not found");

            _dbContext.Companies.Remove(company);
            _dbContext.SaveChanges();
        }

        public IEnumerable<CompanyDto> Search(SearchCompanyDto dto)
        {
            if(dto.EmployeeDateOfBirthFrom is not null && dto.EmployeeDateOfBirthTo is not null)
                if(dto.EmployeeDateOfBirthTo < dto.EmployeeDateOfBirthFrom)
                {
                    throw new BadRequestException("Date From is greater than Date To");
                }

            var allCompanies = _dbContext
                .Companies
                .Include(c => c.Employees)
                .ToList();

            var filteredCompanies = new List<Company>();

            foreach(var company in allCompanies)
            {
                if(dto.Keyword is not null)
                {
                    if(company.Name == dto.Keyword)
                    {
                        filteredCompanies.Add(company);
                        break;
                    }

                    foreach(var employeer in company.Employees)
                    {
                        if(employeer.FirstName == dto.Keyword || employeer.LastName == dto.Keyword)
                        {
                            filteredCompanies.Add(company);
                            break;
                        }
                    }
                }

                foreach(var employeer in company.Employees)
                {
                    if(dto.EmployeeDateOfBirthFrom <= employeer.DateOfBirth && employeer.DateOfBirth <= dto.EmployeeDateOfBirthTo)
                    {
                        filteredCompanies.Add(company);
                        break;
                    }

                    if(dto.EmployeeJobTitles is not null && dto.EmployeeJobTitles.Contains(employeer.JobTitle))
                    {
                        filteredCompanies.Add(company);
                        break;
                    }
                }
            }

            if(filteredCompanies is null)
                throw new NotFoundException("Company not found");

            var result = _mapper.Map<List<CompanyDto>>(filteredCompanies);
            return result;
        }
    }
}
