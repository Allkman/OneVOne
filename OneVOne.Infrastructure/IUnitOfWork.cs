using OneVOne.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository GameRepository { get; }
        IPersonRepository PersonRepository { get; }
        IPlayByPlayStatisticsRepository PlayByPlayStatisticsRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        ITeamRepository TeamRepository { get; }
        IUserRepository UserRepository { get; }
        IPlayerImageRepository PlayerImageRepository { get; }
        Task CommitAsync();
    }
}
