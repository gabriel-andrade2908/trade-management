using GerenciamentoComercio_Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.Utils.IRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GerenciamentoComercioContext _context;
        protected readonly DbSet<TEntity> _dataSet;

        public Repository(GerenciamentoComercioContext context)
        {
            _context = context;
            _dataSet = _context.Set<TEntity>();
        }

        public void AddNew(TEntity model)
        {
            _dataSet.Add(model);
        }

        public async virtual Task<TEntity> GetById(int id)
        {
            return _dataSet.Find(id);
        }

        public async virtual Task<IEnumerable<TEntity>> GetMany()
        {
            return await _dataSet.ToListAsync();
        }

        public void Update(TEntity model)
        {
            _dataSet.Update(model);
        }

        public void Delete(TEntity model)
        {
            _dataSet.Remove(model);
        }

        public void DeleteSeveral(IEnumerable<TEntity> objs)
        {
            foreach (var obj in objs)
                _dataSet.Remove(obj);
        }
    }
}
