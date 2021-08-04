using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class ClientTransactionProduct
    {
        public int Id { get; set; }
        public int? IdClientTransaction { get; set; }
        public int? IdProduct { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual ClientTransaction IdClientTransactionNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
