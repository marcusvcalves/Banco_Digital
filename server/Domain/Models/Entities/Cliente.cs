using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Entities
{
    public enum TipoCliente
    {
        Comum,
        Super,
        Premium
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }

        [JsonIgnore]
        public TipoCliente TipoCliente { get; set; }

        [NotMapped]
        [JsonPropertyName("TipoCliente")]
        public string TipoClienteString
        {
            get => TipoCliente.ToString();
            set => TipoCliente = (TipoCliente)Enum.Parse(typeof(TipoCliente), value, ignoreCase: true);
        }

        public ICollection<Conta> Contas { get; set; } = new List<Conta>();
    }
}