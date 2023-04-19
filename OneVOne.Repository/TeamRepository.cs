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
    internal sealed class TeamRepository : RepositoryId<Team>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context)
        {
        }
    }
}
