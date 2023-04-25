using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Services.Interfaces
{
    public interface IGameService
    {
        Task CoinToss(Guid playerOne, Guid playerTwo);
        Task<Game> PlayGame(Guid playerOne, Guid playerTwo, string gameTime);
        Task<Game> GetGame(string gameTime);
    }
}
