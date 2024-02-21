using Domain.Models.Entities;

namespace Domain.Models.DTO.CartaoDTO;

public class UpdateCartaoDto
{
    public StatusCartao StatusCartao { get; set; }
    public string Senha { get; set; }
}
