using GLMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceRequest>()
                .Property(s => s.AmountUsd)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServiceRequest>()
                .Property(s => s.AmountZar)
                .HasPrecision(18, 2);
        }
    }
}