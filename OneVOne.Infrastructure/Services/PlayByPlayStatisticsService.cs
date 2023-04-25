using OneVOne.GameService.Infrastructure.Models;
using OneVOne.GameService.Infrastructure.Repositories;
using OneVOne.GameService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Services
{
    public class PlayByPlayStatisticsService : IPlayByPlayStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlayByPlayStatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
