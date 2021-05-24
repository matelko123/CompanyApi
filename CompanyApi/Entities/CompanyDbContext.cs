using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Entities
{
    public class CompanyDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=Company;Trusted_Connection=True;";

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employeer> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Company>()
                .Property(r => r.EstablishmentYear)
                .IsRequired();
        }

        // Connection do DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
