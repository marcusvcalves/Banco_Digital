namespace Domain.Models.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Password { get; set; }
        public decimal? Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<Card>? Cards { get; } = new List<Card>();
    }
    

    public class CheckingAccount : Account
    {
        public decimal MonthlyFee { get; set; } = 2;
    }

    public class SavingsAccount : Account
    {
        public decimal ReturnRates { get; set; } = 0.5m;
    }
}