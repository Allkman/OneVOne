﻿using OneVOne.GameService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure
{
    public interface IRepositoryId<TEntity> : IRepository<TEntity> where TEntity : EntityId
    {
        Task<TEntity> FindAsync(Guid? id);
    }
}
