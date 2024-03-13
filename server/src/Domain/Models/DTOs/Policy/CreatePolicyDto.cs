namespace Domain.Models.DTOs;

public class CreatePolicyDto
{
    public string? Number { get; set; }
    public DateTime HiringDate { get; set; }
    public decimal Value { get; set; }
    public string? TriggeringDescription { get; set; }
    public int CreditCardId { get; set; }
}