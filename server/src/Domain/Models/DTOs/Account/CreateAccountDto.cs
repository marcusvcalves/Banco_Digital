using System.Text.Json.Serialization;
using Domain.Models.Enums;

namespace Domain.Models.DTOs;

public class CreateAccountDto
{
    public string? Number { get; set; }
    public string? Password { get; set; }
    public decimal? Balance { get; set; }
    public DateTime CreationDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountType AccountType { get; set; }
    public int ClientId { get; set; }
}