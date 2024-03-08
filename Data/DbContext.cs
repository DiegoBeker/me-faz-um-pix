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

    // map 1:n payment provider <-> payment provider account relation
    builder.Entity<PaymentProvider>().HasKey(e => e.Id);
    builder.Entity<PaymentProviderAccount>().HasKey(e => e.Id);
    builder.Entity<PaymentProvider>()
      .HasMany(e => e.PaymentProviderAccounts)
      .WithOne(e => e.PaymentProvider)
      .HasForeignKey(e => e.PaymentProviderId);
    
    // map 1:n payment provider account <-> pix key relation
    builder.Entity<PaymentProviderAccount>().HasKey(e => e.Id);
    builder.Entity<PixKey>().HasKey(e => e.Id);
    builder.Entity<PaymentProviderAccount>()
      .HasMany(e => e.PixKeys)
      .WithOne(e => e.PaymentProviderAccount)
      .HasForeignKey(e => e.PaymentProviderAccountId);
  }
}