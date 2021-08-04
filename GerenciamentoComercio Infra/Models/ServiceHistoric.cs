using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class ServiceHistoric
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdService { get; set; }
        public decimal? Sla { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual Service IdServiceNavigation { get; set; }
    }
}
