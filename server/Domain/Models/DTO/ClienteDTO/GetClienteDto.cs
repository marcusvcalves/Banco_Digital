using Domain.Models.Entities;

namespace Domain.Models.DTO.ClienteDTO;

public class GetClienteDto
{
    public int Id { get; set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
    public string TipoCliente { get; set; }
}