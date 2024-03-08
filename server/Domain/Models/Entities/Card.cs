namespace Domain.Models.Entities
{
    
    public class Card
    {
        public int Id { get; set; }
        public bool ActiveCard { get; set; }
        public string? Number { get; set; }
        public string? Password { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }

    public class DebitCard : Card
    {
        public decimal DailyLimit { get; set; }
        public ICollection<Policy>? Policies { get; } = new List<Policy>();
    }
    
    public class CreditCard : Card
    {
        public decimal CreditLimit { get; set; }
    }
}