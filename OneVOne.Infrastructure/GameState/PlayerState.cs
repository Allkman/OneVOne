using OneVOne.Common;
using OneVOne.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.Infrastructure.GameState
{
    public class PlayerState : IGameState
    {
        private Random _randomNumberGenerator;
        private const byte LowerBoundOfRating = 10;

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

        public byte ScoringAttempt(byte? playerOne, byte? playerTwo, byte scoringPointAttempt)
        {
            byte differenceOnAttackAttempt = AbsDifference(playerOne, playerTwo);

            if (playerOne > playerTwo)
            {
                var attackerRandomScoringChanceNumber = 
                    _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnAttackAttempt);
                var deffenderRandomDeffenceNumber = 
                    _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                while (attackerRandomScoringChanceNumber == deffenderRandomDeffenceNumber)
                {
                    attackerRandomScoringChanceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnAttackAttempt);
                    deffenderRandomDeffenceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                }
                return ShotAttempt(attackerRandomScoringChanceNumber, deffenderRandomDeffenceNumber, scoringPointAttempt);
            }
            else
            {
                var attackerRandomScoringChanceNumber = 
                    _randomNumberGenerator.Randomize(0, LowerBoundOfRating );
                var deffenderRandomDeffenceNumber = 
                    _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnAttackAttempt);
                while (attackerRandomScoringChanceNumber == deffenderRandomDeffenceNumber)
                {
                    attackerRandomScoringChanceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                    deffenderRandomDeffenceNumber = 
                        _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnAttackAttempt);
                }
                return ShotAttempt(attackerRandomScoringChanceNumber, deffenderRandomDeffenceNumber, scoringPointAttempt);
            }
        }

        private byte ShotAttempt(byte playerOneRandom, byte playerTwoRandom, byte scoringPointAttempt)
        {
            if(scoringPointAttempt == 1) 
                return (playerOneRandom > playerTwoRandom) ? Convert.ToByte(1) : Convert.ToByte(0);
            else
                return (playerOneRandom > playerTwoRandom) ? Convert.ToByte(2) : Convert.ToByte(0);

        }

        public byte Rebound(byte? playerOne, byte? playerTwo)
        {
            byte differenceOnRebuoundAttempt = AbsDifference(playerOne, playerTwo);
            if (playerOne > playerTwo)
            {
                var offensiveReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnRebuoundAttempt);
                var deffensiveReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating);

                while (offensiveReboundRandomNumber == deffensiveReboundRandomNumber)
                {
                    offensiveReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating + differenceOnRebuoundAttempt);
                    deffensiveReboundRandomNumber = _randomNumberGenerator.Randomize(0, LowerBoundOfRating);
                }
                if (offensiveReboundRandomNumber > deffensiveReboundRandomNumber)
                {
                    playerOne++;
                }
                else
                {
                    playerTwo++;
                }
            }
            else
            {

            }
            return 0;
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
