using Domain.Models.Entities;

namespace Domain.Models.DTOs;

public class CreateClientDto
{
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Cpf { get; set; }
    public string? Address { get; set; }
    public ClientType ClientType { get; set; }
}