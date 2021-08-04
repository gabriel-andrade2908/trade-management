using System;

namespace GerenciamentoComercio_Domain.Utils.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
