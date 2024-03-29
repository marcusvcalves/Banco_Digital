﻿namespace Domain.Models.DTOs;

public class GetPolicyDto
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public DateTime HiringDate { get; set; }
    public decimal Value { get; set; }
    public string? TriggeringDescription { get; set; }
    public GetCreditCardDto? CreditCard { get; set; }
}