using Microsoft.AspNetCore.Mvc;
using OneVOne.Core.Entities;
using OneVOne.Infrastructure;
using OneVOne.Infrastructure.Services.Interfaces;

namespace OneVOne.WebAPI.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamService.GetAllTeamsAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<Team> GetAsync(Guid id)
        {
            return await _teamService.GetTeamAsync(id);
        }
    }
}
