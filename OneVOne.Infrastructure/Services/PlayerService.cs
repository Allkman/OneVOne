using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Repositories;
using OneVOne.Infrastructure.Services.Interfaces;

namespace OneVOne.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _unitOfWork.PlayerRepository.GetAllAsync();
        }

        public async Task<Player> GetPlayerAsync(Guid playerId)
        {
            return await _unitOfWork.PlayerRepository.FindAsync(playerId);
        }

        public async Task<IEnumerable<Player>> GetAllTeamPlayersAsync(Guid teamId)
        {
            return await _unitOfWork.PlayerRepository.FilterAsync(x => x.TeamId == teamId);
        }

    }
}
