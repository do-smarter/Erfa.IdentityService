using Erfa.IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Erfa.IdentityService.Models
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.seedRoles(builder);
        }
        public DbSet<Employee> Employees { get; set; }
        private void seedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "ProdAdmin", ConcurrencyStamp = "1", NormalizedName = "PRODADMIN" },
                new IdentityRole() { Name = "ProdWorker", ConcurrencyStamp = "2", NormalizedName = "PRODWORKER" },
                new IdentityRole() { Name = "HRAdmin", ConcurrencyStamp = "2", NormalizedName = "HRADMIN" }
                );
        }
    }
}
