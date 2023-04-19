using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Core.Entities
{
    public class Game : EntityId
    {
        public virtual Player PlayerOne { get; set; }
        public byte PlayerOneTotalScore { get; set; }
        public bool PlayerOneWin { get; set; }
        public bool PlayerTwoWin { get; set; }
        public byte PlayerTwoTotalScore { get; set; }
        public virtual Player PlayerTwo { get; set; }

        public Guid UserId { get; set; }

        public virtual PlayByPlayStatistics PlayerOneStatistics { get; set; }
        public virtual PlayByPlayStatistics PlayerTwoStatistics { get; set; }
    }
}
