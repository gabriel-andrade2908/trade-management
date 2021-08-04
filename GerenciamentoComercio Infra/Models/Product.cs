using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Product
    {
        public Product()
        {
            ClientTransactionProducts = new HashSet<ClientTransactionProduct>();
            ProductHistorics = new HashSet<ProductHistoric>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IdProductCategory { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual ProductCategory IdProductCategoryNavigation { get; set; }
        public virtual ICollection<ClientTransactionProduct> ClientTransactionProducts { get; set; }
        public virtual ICollection<ProductHistoric> ProductHistorics { get; set; }
    }
}
