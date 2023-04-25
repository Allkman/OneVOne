using OneVOne.GameService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Team> GetTeamAsync(Guid id);
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> FindTeamByAbbreviationAsync(string abbr);
    }
}
