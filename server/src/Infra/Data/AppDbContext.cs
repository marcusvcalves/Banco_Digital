using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
    
namespace Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Policy> Policies { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Policy>()
            .Property(a => a.Value)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Client>()
            .Property(c => c.ClientType)
            .HasConversion<string>();

        modelBuilder.Entity<Account>()
            .HasDiscriminator<string>("accountType")
            .HasValue<CheckingAccount>("checking")
            .HasValue<SavingsAccount>("savings");
        
        modelBuilder.Entity<Account>()
            .Property(c => c.Balance)
            .HasColumnType("decimal(18, 2)");
        
        modelBuilder.Entity<CheckingAccount>()
            .Property(c => c.MonthlyFee)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<SavingsAccount>()
            .Property(c => c.ReturnRates)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Card>()
            .HasDiscriminator<string>("cardType")
            .HasValue<DebitCard>("debito")
            .HasValue<CreditCard>("credito");
        

        modelBuilder.Entity<DebitCard>()
            .Property(c => c.DailyLimit)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<CreditCard>()
            .Property(c => c.CreditLimit)
            .HasColumnType("decimal(18, 2)");
    }
}