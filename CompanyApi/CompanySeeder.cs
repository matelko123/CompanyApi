using CompanyApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApi
{
    public class CompanySeeder
    {
        private readonly CompanyDbContext _dbContext;

        public CompanySeeder(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Companies.Any())
                {
                    var companies = GetCompanies();
                    _dbContext.Companies.AddRange(companies);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Company> GetCompanies()
        {
            var companies = new List<Company>()
            {
                new Company()
                {
                    Name = "Google",
                    EstablishmentYear = 1998,
                    Employees = new List<Employeer>()
                    {
                        new Employeer()
                        {
                            FirstName = "Mateusz",
                            LastName = "Gutowski",
                            DateOfBirth = new DateTime(1998,7,28),
                            JobTitle = JobTitles.Developer
                        },
                        new Employeer()
                        {
                            FirstName = "Wiktor",
                            LastName = "Nowak",
                            DateOfBirth = new DateTime(1996,11,12),
                            JobTitle = JobTitles.Manager
                        }
                    }
                },
                new Company()
                {
                    Name = "Facebook",
                    EstablishmentYear = 2004,
                    Employees = new List<Employeer>()
                    {
                        new Employeer()
                        {
                            FirstName = "Amelia",
                            LastName = "Nowak",
                            DateOfBirth = new DateTime(1970,1,1),
                            JobTitle = JobTitles.Developer
                        },
                        new Employeer()
                        {
                            FirstName = "Paweł",
                            LastName = "Szulc",
                            DateOfBirth = new DateTime(2000,4,27),
                            JobTitle = JobTitles.Architect
                        }
                    }
                }

            };
            return companies;
        }
    }
}
