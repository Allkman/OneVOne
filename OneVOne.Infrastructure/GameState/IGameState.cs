using OneVOne.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.GameState
{
    public interface IGameState
    {
        byte ScoringAttempt(byte? playerOne, byte? playerTwo, byte scoringPointAttempt);
        byte Rebound(byte? playerOne, byte? playerTwo);
        void Block();
        void Foul();
    }
}
