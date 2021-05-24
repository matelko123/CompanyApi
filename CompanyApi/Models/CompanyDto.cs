using CompanyApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApi.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public List<EmployeerDto> Employees { get; set; }
    }
}
