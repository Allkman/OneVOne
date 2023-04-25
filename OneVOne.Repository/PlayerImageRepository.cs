using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure;
using OneVOne.GameService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Repository
{
    internal sealed class PlayerImageRepository : RepositoryId<PlayerImage>, IPlayerImageRepository
    {
        public PlayerImageRepository(DataContext context) : base(context)
        {
        }
    }
}
