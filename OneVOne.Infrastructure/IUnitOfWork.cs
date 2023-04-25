using OneVOne.GameService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository GameRepository { get; }
        IPersonRepository PersonRepository { get; }
        IPlayByPlayStatisticsRepository PlayByPlayStatisticsRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        ITeamRepository TeamRepository { get; }
        IPlayerImageRepository PlayerImageRepository { get; }
        Task CommitAsync();
    }
}
