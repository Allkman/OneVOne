using Microsoft.EntityFrameworkCore;
using OneVOne.Core.Entities;
using OneVOne.Infrastructure;
using OneVOne.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Repository
{
    internal sealed class PlayerRepository : RepositoryId<Player>, IPlayerRepository
    {
        public PlayerRepository(DataContext context) : base(context)
        {
        }
             
    }
}
