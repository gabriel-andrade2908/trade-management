using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class ProductHistoric
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdProduct { get; set; }
        public decimal? Price { get; set; }
        public long? Quantity { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
