using OneVOne.GameService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> GetPlayerAsync(Guid id);
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<IEnumerable<Player>> GetAllTeamPlayersAsync(Guid teamId);
    }
}
