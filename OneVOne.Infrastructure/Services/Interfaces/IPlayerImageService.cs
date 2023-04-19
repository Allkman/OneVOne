using OneVOne.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services.Interfaces
{
    public interface IPlayerImageService
    {
        Task<PlayerImage> GetPlayerImageAsync(Guid? playerId);
    }
}
