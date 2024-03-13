namespace Domain.Models.DTOs;

public class CreateCardDto
{
    public string? Number { get; set; }
    public string? Password { get; set; }
    public string? CardType { get; set; }
    public bool ActiveCard { get; set; }
    public int AccountId { get; set; }
}