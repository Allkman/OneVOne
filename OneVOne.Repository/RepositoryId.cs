using OneVOne.Core;
using OneVOne.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Repository
{
    internal abstract class RepositoryId<TEntity> : Repository<TEntity>, IRepositoryId<TEntity> where TEntity : EntityId
    {
        protected RepositoryId(DataContext context) : base(context)
        {
        }

        public async Task<TEntity> FindAsync(Guid? id)
        {
            var entity = await FindAsync(e => e.Id == id);
            return entity;
        }
    }
}
