using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Access
    {
        public Access()
        {
            EmployeeAccesses = new HashSet<EmployeeAccess>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual ICollection<EmployeeAccess> EmployeeAccesses { get; set; }
    }
}
