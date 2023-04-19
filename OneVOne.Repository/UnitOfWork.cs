using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using OneVOne.Infrastructure;
using OneVOne.Infrastructure.Repositories;
using DataContext = OneVOne.Infrastructure.DataContext;

namespace OneVOne.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextOptions _options;
        private DataContext _context;

        private bool _disposed;

        private GameRepository _gameRepository;
        private PersonRepository _personRepository;
        private PlayByPlayStatisticsRepository _playByPlayStatisticsRepository;
        private PlayerRepository _playerRepository;
        private TeamRepository _teamRepository;
        private UserRepository _userRepository;
        private PlayerImageRepository _playerImageRepository;
        public UnitOfWork(IOptions<UnitOfWorkOptions> options) : this(options.Value)
        {

        }
        public UnitOfWork(UnitOfWorkOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(options.ConnectionString, x => x.CommandTimeout(options.Timeout));
            _options = optionsBuilder.Options;
        }

        private DataContext Context => _context ??= new DataContext(_options);
        public IGameRepository GameRepository => _gameRepository ??= new GameRepository(Context);

        public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(Context);

        public IPlayByPlayStatisticsRepository PlayByPlayStatisticsRepository => _playByPlayStatisticsRepository ??= new PlayByPlayStatisticsRepository(Context);

        public IPlayerRepository PlayerRepository => _playerRepository ??= new PlayerRepository(Context);

        public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(Context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(Context);
        public IPlayerImageRepository PlayerImageRepository => _playerImageRepository ??= new PlayerImageRepository(Context);

        public async Task CommitAsync()
        {
            if (_context is null) return;

            if (_disposed) throw new ObjectDisposedException("UnitOfWork");
             
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context is null || _disposed)
                return;

            _context.Dispose();
            _disposed = true;
        }
    }
}