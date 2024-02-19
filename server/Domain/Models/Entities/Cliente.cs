using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public abstract class Cliente
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public ICollection<Conta> Contas { get; } = new List<Conta>();
    }

    public class ClienteComum : Cliente
    {
        
    }

    public class ClienteSuper : Cliente
    {
        
    }

    public class ClientePremium : Cliente
    {
        
    }
}