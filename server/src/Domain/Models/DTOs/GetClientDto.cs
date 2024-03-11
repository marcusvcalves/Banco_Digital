using Domain.Models.Entities;

namespace Domain.Models.DTOs;

public class GetClientDto
{
    public int Id { get; set; }
    public string? Cpf { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Address { get; set; }
    public string? ClientType { get; set; }
}