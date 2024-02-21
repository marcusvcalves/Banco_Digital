namespace Domain.Models.DTO.ClienteDTO;

public class UpdateClienteDto
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
}