using Microsoft.AspNetCore.Mvc;
using PlayerData.WebAPI.Services;
using System.Threading.Tasks;

namespace PlayerData.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonDataController : Controller
    {
        private readonly IPersonsDBChromeDriverService _service;
        private readonly IPlayerStatsToDBChromeDriverService _playerStatsService;
        public PersonDataController(IPersonsDBChromeDriverService service, IPlayerStatsToDBChromeDriverService playerStatsService)
        {
            _service = service;
            _playerStatsService = playerStatsService;
        }
        [HttpPost("/persondata")]
        public async Task<ActionResult> PostPersonData()
        {
            await _service.ExecuteChromeDriverForPersonsDbTable();
            return Ok();
        }

        [HttpPut("/playerstats")]
        public async Task<ActionResult> PostPlayerStatistics()
        {
            await _playerStatsService.ExecuteChromeDriverForPlayerStatsToDbTable();
            return Ok();
        }

        [HttpPut("/playersimage")]
        public async Task<ActionResult> PostPlayersImage()
        {
            await _service.GetPlayersImage();
            return Ok();
        }
    }
}
