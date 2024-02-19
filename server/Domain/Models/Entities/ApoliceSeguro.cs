using System;

namespace Domain.Models.Entities
{
    public class ApoliceSeguro
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal Valor { get; set; }
        public string DescricaoAcionamento { get; set; }
        public int CartaoId { get; set; }
        public Cartao Cartao { get; set; }
    }
}