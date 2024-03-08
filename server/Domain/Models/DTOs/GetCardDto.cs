using Domain.Models.Entities;

namespace Domain.Models.DTOs;

public class GetCardDto
{
    public bool ActiveCard { get; set; }
    public string? Number { get; set; }
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    
}