using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Services.AuthAPI.Models;

// NOTE: This is currently using a type already implemented by the pkg.
namespace Services.AuthAPI.Data {
    public class AppDbContext : IdentityDbContext<ApplicationUser> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // TODO: This can be commented out before any migration is made. I'll
        // leave it in so I can investigate and step aside the course a little
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}