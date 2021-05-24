using System;

namespace CompanyApi.Entities
{
    public class Employeer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitles JobTitle { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
