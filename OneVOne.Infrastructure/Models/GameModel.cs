using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Models
{
    public class GameModel : Model
    {
        public PlayerModel PlayerOne { get; set; }
        public byte PlayerOneScore { get; set; }
        public bool PlayerOneWin { get; set; }
        public bool PlayerTwoWin { get; set; }
        public byte PlayerTwoScore { get; set; }
        public PlayerModel PlayerTwo { get; set; }

        public Guid UserId { get; set; }

        public PlayByPlayStatisticsModel PlayByPlayStatistics { get; set; }
    }
}
