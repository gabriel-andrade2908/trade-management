﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GerenciamentoComercio_Infra.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientTransaction = new HashSet<ClientTransaction>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ClientTransaction> ClientTransaction { get; set; }
    }
}
