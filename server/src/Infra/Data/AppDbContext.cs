using Microsoft.EntityFrameworkCore;
using Domain.Models.Enums;
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
            .Property(policy => policy.Value)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Client>()
            .Property(client => client.ClientType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Account>()
            .Property(acc => acc.Balance)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Account>()
            .Property(acc => acc.AccountType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Account>()
            .HasDiscriminator<AccountType>("AccountType")
            .HasValue<Account>(AccountType.Common)
            .HasValue<CheckingAccount>(AccountType.Checking)
            .HasValue<SavingsAccount>(AccountType.Savings);

        modelBuilder.Entity<CheckingAccount>()
            .Property(acc => acc.MonthlyFee)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<SavingsAccount>()
            .Property(acc => acc.ReturnRates)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Card>()
            .Property(card => card.CardType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Card>()
            .HasDiscriminator<CardType>("CardType")
            .HasValue<Card>(CardType.Normal)
            .HasValue<DebitCard>(CardType.Debit)
            .HasValue<CreditCard>(CardType.Credit);

        modelBuilder.Entity<DebitCard>()
            .Property(card => card.DailyLimit)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<CreditCard>()
            .Property(card => card.CreditLimit)
            .HasColumnType("decimal(18, 2)");
    }
}