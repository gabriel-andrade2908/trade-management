﻿using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public Client GetClientByEmail(string email)
        {
            return _context.Client.FirstOrDefault(x => x.Email == email);
        }
    }
}
