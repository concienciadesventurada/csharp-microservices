using Microsoft.EntityFrameworkCore;

using Services.CouponAPI.Models;

namespace Services.CouponAPI.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // NOTE: DBSET => would be the table name on the database
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                CouponId = 1,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                CouponId = 2,
                CouponCode = "20OFF",
                DiscountAmount = 10,
                MinAmount = 400
            });
        }
    }
}