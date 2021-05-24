using System.Collections.Generic;

namespace CompanyApi.Entities
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }

        public virtual List<Employeer> Employees { get; set; }
    }
}
