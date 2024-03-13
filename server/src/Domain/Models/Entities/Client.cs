using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models.Entities
{
    public enum ClientType
    {
        Common = 0,
        Super = 1,
        Premium = 2
    }

    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Cpf { get; set; }
        public string? Address { get; set; }

        [JsonIgnore]
        public ClientType ClientType { get; set; }

        [NotMapped]
        [JsonPropertyName("ClientType")]
        public string ClientTypeString
        {
            get => ClientType.ToString();
            set => ClientType = (ClientType)Enum.Parse(typeof(ClientType), value, ignoreCase: true);
        }

        public ICollection<Account>? Accounts { get; set; } = new List<Account>();
    }
}