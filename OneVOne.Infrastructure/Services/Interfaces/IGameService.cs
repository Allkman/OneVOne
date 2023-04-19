using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services.Interfaces
{
    public interface IGameService
    {
        Task CoinToss(Guid playerOne, Guid playerTwo);
        Task<Game> PlayGame(Guid playerOne, Guid playerTwo);
    }
}
