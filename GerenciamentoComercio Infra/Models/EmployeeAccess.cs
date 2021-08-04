using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class EmployeeAccess
    {
        public int Id { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdAccess { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }

        public virtual Access IdAccessNavigation { get; set; }
        public virtual Employee IdEmployeeNavigation { get; set; }
    }
}
