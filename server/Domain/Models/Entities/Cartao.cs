using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public enum StatusCartao
    {
        Ativado,
        Desativado
    }

    public class Cartao
    {
        public int Id { get; set; }
        public StatusCartao StatusCartao { get; set; }
        public string Numero { get; set; }
        public string Senha { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; } = null;
    }

    public class CartaoDebito : Cartao
    {
        public decimal LimiteDiario { get; set; }
        public ICollection<Apolice> Apolices { get; } = new List<Apolice>();
    }
    
    public class CartaoCredito : Cartao
    {
        public decimal LimiteCredito { get; set; }
    }
}