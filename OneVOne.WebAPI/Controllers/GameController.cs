using Microsoft.AspNetCore.Mvc;
using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure.Services.Interfaces;
using System.Net;

namespace OneVOne.GameService.WebAPI.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpPut("CoinToss")]
        public async Task<IActionResult> CoinToss(Guid playerOneId, Guid playerTwoId)
        {
            try
            {
                await _gameService.CoinToss(playerOneId, playerTwoId);
                return Ok();
            }
            catch (HttpRequestException e)
            when(e.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(e.Message);
            }
            catch (HttpRequestException e)
            when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }
        [HttpPost("PlayGame")]
        public async Task<Game> PlayGame(Guid playerOneId, Guid playerTwoId, string gameTime)
        {
            return await _gameService.PlayGame(playerOneId, playerTwoId, gameTime);
        }
        [HttpGet]
        public async Task<Game> GetGame(string gameTime)
        {
            return await _gameService.GetGame(gameTime);
        }
    }
}
