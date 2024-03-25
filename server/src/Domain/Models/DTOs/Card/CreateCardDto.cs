using System.Text.Json.Serialization;
using Domain.Models.Enums;

namespace Domain.Models.DTOs;

public class CreateCardDto
{
    public string? Number { get; set; }
    public string? Password { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CardType CardType { get; set; }
    public bool ActiveCard { get; set; }
    public int AccountId { get; set; }
}