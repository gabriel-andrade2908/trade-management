using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class ClientTransaction
    {
        public ClientTransaction()
        {
            ClientTransactionProduct = new HashSet<ClientTransactionProduct>();
            ClientTransactionService = new HashSet<ClientTransactionService>();
        }

        public int Id { get; set; }
        public int? IdClient { get; set; }
        public int? IdEmployee { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public double? DiscountPercentage { get; set; }
        public string Observations { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual ICollection<ClientTransactionProduct> ClientTransactionProduct { get; set; }
        public virtual ICollection<ClientTransactionService> ClientTransactionService { get; set; }
    }
}
