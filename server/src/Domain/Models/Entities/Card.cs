using Domain.Models.Enums;

namespace Domain.Models.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Password { get; set; }
        public bool ActiveCard { get; set; }
        public virtual CardType CardType { get; set; } = CardType.Normal;
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }

    public class DebitCard : Card
    {
        public decimal DailyLimit { get; set; }
        public ICollection<Policy>? Policies { get; } = new List<Policy>();
        public override CardType CardType { get; set; } = CardType.Debit;
    }
    
    public class CreditCard : Card
    {
        public decimal CreditLimit { get; set; }
        public override CardType CardType { get; set; } = CardType.Credit;
    }
}