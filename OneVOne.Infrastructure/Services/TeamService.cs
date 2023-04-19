using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Repositories;
using OneVOne.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _unitOfWork.TeamRepository.GetAllAsync();
        }

        public async Task<Team> GetTeamAsync(Guid id)
        {
            return await _unitOfWork.TeamRepository.FindAsync(id);
        }

        public async Task<Team> FindTeamAsync(Guid id)
        {
            return await _unitOfWork.TeamRepository.FindAsync(x => x.Id == id);
        }

        public async Task<Team> FindTeamByAbbreviationAsync(string abbr)
        {
            return await _unitOfWork.TeamRepository.FindAsync(x => x.Abbreviation == abbr);
        }
    }
}
