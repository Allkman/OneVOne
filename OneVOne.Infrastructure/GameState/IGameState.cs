using OneVOne.GameService.Core.Entities;

namespace OneVOne.GameService.Infrastructure.GameState
{
    public interface IGameState
    {
        byte ScoringAttempt(byte? attacker, byte? deffender, byte scoringPointAttempt);
        bool Rebound(byte? attacker, byte? deffender);
        void Block();
        void Foul();
        void Fatigue(Player player, int roundCount);
    }
}
