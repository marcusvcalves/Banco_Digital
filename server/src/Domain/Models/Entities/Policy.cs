﻿namespace Domain.Models.Entities
{
    public class Policy
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal Value { get; set; }
        public string? TriggeringDescription { get; set; }
        public int CreditCardId { get; set; }
        public CreditCard? CreditCard { get; set; }
    }
}