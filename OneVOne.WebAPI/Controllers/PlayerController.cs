﻿using Microsoft.AspNetCore.Mvc;
using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure;
using OneVOne.GameService.Infrastructure.Services.Interfaces;

namespace OneVOne.GameService.WebAPI.Controllers
{
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllAsync()
        {
            return (await _playerService.GetAllPlayersAsync()).ToList();
        }

        [HttpGet("{playerId:guid}")]
        public async Task<ActionResult<Player>> GetAsync(Guid playerId)
        {
            return await _playerService.GetPlayerAsync(playerId);
        }

        [HttpGet("teamplayers/{teamId:guid}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllTeamPlayersAsync(Guid teamId)
        {
            return (await _playerService.GetAllTeamPlayersAsync(teamId)).ToList();
        }
    }
}
