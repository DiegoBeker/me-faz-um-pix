using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Data;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    public DbSet<PaymentProviderAccount> PaymentProviderAccount { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
  {
    // map 1:n user <-> payment provider account relation
    builder.Entity<User>().HasKey(e => e.Id);
    builder.Entity<PaymentProviderAccount>().HasKey(e => e.Id);
    builder.Entity<User>()
      .HasMany(e => e.PaymentProviderAccounts)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId);
  }
}