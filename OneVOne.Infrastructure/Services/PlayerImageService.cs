using OneVOne.Core.Entities;
using OneVOne.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services
{
    public class PlayerImageService : IPlayerImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlayerImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PlayerImage> GetPlayerImageAsync(Guid? playerId)
        {
            var image = await _unitOfWork.PlayerImageRepository.FindAsync(image => image.PlayerId == playerId);
            Console.WriteLine(image.Image);
            return image;
        }
    }
}
