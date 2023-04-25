using Microsoft.EntityFrameworkCore;
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
    internal sealed class PlayerRepository : RepositoryId<Player>, IPlayerRepository
    {
        public PlayerRepository(DataContext context) : base(context)
        {
        }
             
    }
}
