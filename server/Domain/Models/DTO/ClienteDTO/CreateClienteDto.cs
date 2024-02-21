namespace Domain.Models.DTO.ClienteDTO;

public class CreateClienteDto
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
}