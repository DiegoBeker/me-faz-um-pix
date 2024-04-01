using me_faz_um_pix.Models;
using Microsoft.EntityFrameworkCore;

namespace me_faz_um_pix.Data;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
  public DbSet<User> User { get; set; }

  public DbSet<PaymentProvider> PaymentProvider { get; set; }
  public DbSet<PaymentProviderAccount> PaymentProviderAccount { get; set; }
  public DbSet<PixKey> PixKey { get; set; }

  public DbSet<Payment> Payment { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
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

    // map 1:n payment provider account <-> payment relation
    builder.Entity<PaymentProviderAccount>().HasKey(e => e.Id);
    builder.Entity<Payment>().HasKey(e => e.Id);
    builder.Entity<PaymentProviderAccount>()
      .HasMany(e => e.Payments)
      .WithOne(e => e.PaymentProviderAccount)
      .HasForeignKey(e => e.PaymentProviderAccountId);

     // map 1:n pix key <-> payment relation
    builder.Entity<PixKey>().HasKey(e => e.Id);
    builder.Entity<Payment>().HasKey(e => e.Id);
    builder.Entity<PixKey>()
      .HasMany(e => e.Payments)
      .WithOne(e => e.PixKey)
      .HasForeignKey(e => e.PixKeyId);
    
    // indices
    builder.Entity<User>()
          .HasIndex(e => e.Cpf)
          .IsUnique();
    
    builder.Entity<PixKey>()
          .HasIndex(e => e.Value)
          .IsUnique();
  
    builder.Entity<PaymentProvider>()
          .HasIndex(e => e.Token)
          .IsUnique();
    
    builder.Entity<PaymentProviderAccount>()
        .HasIndex(e => new { e.Agency, e.Number });
  }
}