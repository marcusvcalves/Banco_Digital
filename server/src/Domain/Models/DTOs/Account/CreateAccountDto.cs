namespace Domain.Models.DTOs;

public class CreateAccountDto
{
    public string? Number { get; set; }
    public string? Password { get; set; }
    public decimal? Balance { get; set; }
    public DateTime CreationDate { get; set; }
    public string? AccountType { get; set; }
    public int ClientId { get; set; }
}