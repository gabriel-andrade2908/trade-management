using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ClientTransactions = new HashSet<ClientTransaction>();
            EmployeeAccesses = new HashSet<EmployeeAccess>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Access { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public bool? IsAdministrator { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ClientTransaction> ClientTransactions { get; set; }
        public virtual ICollection<EmployeeAccess> EmployeeAccesses { get; set; }
    }
}
