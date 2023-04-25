using OneVOne.GameService.Core;
using OneVOne.GameService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Repository
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
