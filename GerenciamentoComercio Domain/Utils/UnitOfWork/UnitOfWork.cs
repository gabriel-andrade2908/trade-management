using GerenciamentoComercio_Infra.Context;

namespace GerenciamentoComercio_Domain.Utils.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GerenciamentoComercioContext _context;

        public UnitOfWork(GerenciamentoComercioContext context)
        {
            _context = context;
        }
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
