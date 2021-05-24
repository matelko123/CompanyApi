using CompanyApi.Entities;
using System;
using System.Collections.Generic;

namespace CompanyApi.Models
{
    public class SearchCompanyDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public List<JobTitles> EmployeeJobTitles { get; set; }
    }
}
