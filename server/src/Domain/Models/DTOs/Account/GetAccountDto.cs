namespace Domain.Models.DTOs;

public class GetAccountDto
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public decimal? Balance { get; set; }
    public DateTime CreationDate { get; set; }
    public int ClientId { get; set; }
    public GetClientDto? Client { get; set; }
    public string? AccountType { get; set; }
}