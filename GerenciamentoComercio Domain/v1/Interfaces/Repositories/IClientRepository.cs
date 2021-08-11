﻿using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Infra.Models;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetClientByEmail(string email);
    }
}