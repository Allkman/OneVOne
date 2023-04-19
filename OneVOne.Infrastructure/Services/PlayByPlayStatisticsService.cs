using OneVOne.Infrastructure.Models;
using OneVOne.Infrastructure.Repositories;
using OneVOne.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.Services
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
