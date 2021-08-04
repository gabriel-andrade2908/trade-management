using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientTransactions = new HashSet<ClientTransaction>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ClientTransaction> ClientTransactions { get; set; }
    }
}
