using Microsoft.EntityFrameworkCore;
using OneVOne.GameService.Core;
using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Repository
{
    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        

        public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return await InternalFilter().Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var res = await _dbSet.FirstOrDefaultAsync(predicate); ;
            return res;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await InternalFilter().ToListAsync();
        }

        protected virtual IQueryable<TEntity> InternalFilter()
        {
            return _dbSet;
        }
    }
}
