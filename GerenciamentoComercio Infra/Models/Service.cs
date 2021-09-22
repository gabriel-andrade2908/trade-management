using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Service
    {
        public Service()
        {
            ClientTransactionService = new HashSet<ClientTransactionService>();
            ServiceHistoric = new HashSet<ServiceHistoric>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IdServiceCategory { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Sla { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual ServiceCategory IdServiceCategoryNavigation { get; set; }
        public virtual ICollection<ClientTransactionService> ClientTransactionService { get; set; }
        public virtual ICollection<ServiceHistoric> ServiceHistoric { get; set; }
    }
}
