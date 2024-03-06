using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
    
namespace Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Apolice>? Apolices { get; set; }
    public DbSet<Cartao>? Cartoes { get; set; }
    public DbSet<Cliente>? Clientes { get; set; }
    public DbSet<Conta>? Contas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Apolice>()
            .Property(a => a.Valor)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Cliente>()
            .Property(c => c.TipoCliente)
            .HasConversion<string>();

        modelBuilder.Entity<Conta>()
            .HasDiscriminator<string>("TipoConta")
            .HasValue<ContaCorrente>("corrente")
            .HasValue<ContaPoupanca>("poupanca");
        
        modelBuilder.Entity<Conta>()
            .Property(c => c.Saldo)
            .HasColumnType("decimal(18, 2)");
        
        modelBuilder.Entity<ContaCorrente>()
            .Property(c => c.TaxaMensal)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<ContaPoupanca>()
            .Property(c => c.TaxaRendimento)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Cartao>()
            .HasDiscriminator<string>("TipoCartao")
            .HasValue<CartaoDebito>("debito")
            .HasValue<CartaoCredito>("credito");
        

        modelBuilder.Entity<CartaoDebito>()
            .Property(c => c.LimiteDiario)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<CartaoCredito>()
            .Property(c => c.LimiteCredito)
            .HasColumnType("decimal(18, 2)");
    }
}