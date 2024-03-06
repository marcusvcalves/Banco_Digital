namespace Domain.Models.DTO.ApoliceSeguroDTO;

public class GetApoliceDto
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public DateTime DataContratacao { get; set; }
    public decimal Valor { get; set; }
    public string DescricaoAcionamento { get; set; }
}