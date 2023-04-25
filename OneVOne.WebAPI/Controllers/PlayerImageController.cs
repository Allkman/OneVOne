using Microsoft.AspNetCore.Mvc;
using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure.Services.Interfaces;

namespace OneVOne.GameService.WebAPI.Controllers
{
    public class PlayerImageController : BaseController
    {
        private readonly IPlayerImageService _service;
        public PlayerImageController(IPlayerImageService playerImageService)
        {
            _service = playerImageService;
        }

        [HttpGet]
        public async Task<PlayerImage> GetPlayerImage(Guid? playerId)
        {
            return await _service.GetPlayerImageAsync(playerId);
        }
    }
}
