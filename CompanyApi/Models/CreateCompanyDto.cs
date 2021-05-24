using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyApi.Models
{
    public class CreateCompanyDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int EstablishmentYear { get; set; }

        public List<EmployeerDto> Employees { get; set; }
    }
}
