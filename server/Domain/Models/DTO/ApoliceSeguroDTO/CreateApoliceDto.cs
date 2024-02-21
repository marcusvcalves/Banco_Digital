namespace Domain.Models.DTO.ApoliceSeguroDTO;

public class CreateApoliceDto
{
    public string Numero { get; set; }
    public DateTime DataContratacao { get; set; }
    public decimal Valor { get; set; }
    public string DescricaoAcionamento { get; set; }
    public int CartaoId { get; set; }
    public int ContaId { get; set; }
    public int ClienteId { get; set; }
}