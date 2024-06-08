using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Patient> Patient { get; set; } = default!;
        public DbSet<WebApplication1.Models.ClinicalRecord> ClinicalRecord { get; set; } = default!;
        public DbSet<WebApplication1.Models.Clinic> Clinic { get; set; } = default!;
        public DbSet<WebApplication1.Models.Doctor> Doctor { get; set; } = default!;
    }
}
