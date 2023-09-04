using Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.CouponAPI.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    // NOTE: DBSET => would be the table name on the database
    public DbSet<Coupon> Coupons { get; set; }
  }
}
