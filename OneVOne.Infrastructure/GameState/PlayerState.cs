using OneVOne.Common;
using OneVOne.GameService.Core.Entities;

namespace OneVOne.GameService.Infrastructure.GameState
{
    public class PlayerState : IGameState
    {
        private Random _randomNumberGenerator;
        private const byte LowerBoundOfRating = 10;
        private const byte MinStatValue = 10;
        public PlayerState()
        {
            _randomNumberGenerator = new Random();
        }

        public void Block()
        {
            throw new NotImplementedException();
        }

        public void Foul()
        {
            throw new NotImplementedException();
        }

        public byte ScoringAttempt(byte? attacker, byte? deffender, byte scoringPointAttempt)
        {
            var attackerRandomScoringChanceNumber = 0;
            var deffenderRandomDeffenceNumber = 0;
            if (attacker > deffender)
            {
                while (attackerRandomScoringChanceNumber == deffenderRandomDeffenceNumber)
                {
                    attackerRandomScoringChanceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + AbsDifference(attacker, deffender));
                    deffenderRandomDeffenceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                }
                return ShotAttempt((byte)attackerRandomScoringChanceNumber, (byte)deffenderRandomDeffenceNumber, scoringPointAttempt);
            }
            else
            {
                while (attackerRandomScoringChanceNumber == deffenderRandomDeffenceNumber)
                {
                    attackerRandomScoringChanceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                    deffenderRandomDeffenceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + AbsDifference(attacker, deffender));
                }
                return ShotAttempt((byte)attackerRandomScoringChanceNumber, (byte)deffenderRandomDeffenceNumber, scoringPointAttempt);
            }
        }

        private byte ShotAttempt(byte playerOneRandom, byte playerTwoRandom, byte scoringPointAttempt)
        {
            if(scoringPointAttempt == 1) 
                return (playerOneRandom > playerTwoRandom) ? Convert.ToByte(1) : Convert.ToByte(0);
            else
                return (playerOneRandom > playerTwoRandom) ? Convert.ToByte(2) : Convert.ToByte(0);

        }

        public bool Rebound(byte? attacker, byte? deffender)
        {
            var attackerReboundRandomNumber = 0;
            var deffenderReboundRandomNumber = 0;
            if (attacker > deffender)
            {
                while (attackerReboundRandomNumber == deffenderReboundRandomNumber)
                {
                    attackerReboundRandomNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + AbsDifference(attacker, deffender));
                    deffenderReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                }
                if (attackerReboundRandomNumber > deffenderReboundRandomNumber)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                while (attackerReboundRandomNumber == deffenderReboundRandomNumber)
                {
                    attackerReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                    deffenderReboundRandomNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + AbsDifference(attacker, deffender));
                }
                if (attackerReboundRandomNumber < deffenderReboundRandomNumber)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public void Fatigue(Player player, int roundCount)
        {
            //the higher the athleticism is the less chance is to enter this if to lower the stats
            if (((100 - player.Athleticism) * roundCount)/3 >= _randomNumberGenerator.Randomize(0, 101))
            {
                player.OutsideScoring = DecrementStat(player.OutsideScoring);
                player.InsideScoring = DecrementStat(player.InsideScoring);
                player.Defending = DecrementStat(player.Defending);
                player.Rebounding = DecrementStat(player.Rebounding);
                player.Playmaking = DecrementStat(player.Playmaking);
            }
        }

        private byte DecrementStat(byte? statValue)
        {
            int newStatValue = Math.Max(statValue - 1?? 0, MinStatValue);
            if (newStatValue == 10)
            {
                return (byte)MinStatValue;
            }
            else
            {
                return (byte)newStatValue;
            }
        }

        private byte AbsDifference(byte? playerOne, byte? playerTwo)
        {
            if (playerOne.HasValue && playerTwo.HasValue)
            {
                return Convert.ToByte(Math.Abs(playerOne.Value - playerTwo.Value));
            }
            else
            {
                throw new ArgumentException("Both parameters must have a value.");
            }
        }

    }
}
