using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
