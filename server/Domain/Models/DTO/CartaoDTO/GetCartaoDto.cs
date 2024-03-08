using Domain.Models.Entities;

namespace Domain.Models.DTO.CartaoDTO;

public class GetCartaoDto
{
    public bool CartaoAtivo { get; set; }
    public string? Numero { get; set; }
    public int ContaId { get; set; }
    public Conta? Conta { get; set; }
    
}