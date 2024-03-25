using Domain.Models.Enums;

namespace Domain.Models.DTOs;

public class GetCardDto
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public CardType CardType { get; set; }
    public bool ActiveCard { get; set; }
    public decimal? DailyLimit { get; set; }
    public decimal? CreditLimit { get; set; }
    public int AccountId { get; set; }
    public GetAccountDto? Account { get; set; }
    
}