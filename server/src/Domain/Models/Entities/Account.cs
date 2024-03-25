using Domain.Models.Enums;

namespace Domain.Models.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual AccountType AccountType { get; set; } = AccountType.Common;
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<Card>? Cards { get; } = new List<Card>();
    }
    
    public class CheckingAccount : Account
    {
        public override AccountType AccountType { get; set; } = AccountType.Checking;
        public decimal MonthlyFee { get; } = 2;
    }

    public class SavingsAccount : Account
    {
        public override AccountType AccountType { get; set; } = AccountType.Savings;
        public decimal ReturnRates { get; } = 0.5m;
    }
}