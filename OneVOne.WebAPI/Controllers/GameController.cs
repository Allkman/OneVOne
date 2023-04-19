using Microsoft.AspNetCore.Mvc;
using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Services.Interfaces;

namespace OneVOne.WebAPI.Controllers
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
            catch (ArgumentNullException)
            {
                return BadRequest(new ArgumentNullException());
            }
            catch (Exception)
            {
                return BadRequest(new Exception());
            }
        }
        [HttpPost("PlayGame")]
        public async Task<Game> PlayGame(Guid playerOneId, Guid playerTwoId)
        {
            return await _gameService.PlayGame(playerOneId, playerTwoId);
        }
    }
}
