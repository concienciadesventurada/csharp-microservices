using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// NOTE: This is currently using a type already implemented by the pkg.
namespace Services.AuthAPI.Data {
    public class AppDbContext : IdentityDbContext<IdentityUser> {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // TODO: This can be commented out before any migration is made. I'll
        // leave it in so I can investigate and step aside the course a little
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}