using OneVOne.Common;
using OneVOne.Core.Entities;
using OneVOne.Infrastructure.GameState;
using OneVOne.Infrastructure.Services.Interfaces;

namespace OneVOne.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Random randomNumberGenerator;
        private const byte EndGameScore = 11;

        private readonly IGameState _playerOneState;
        private readonly IGameState _playerTwoState;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            randomNumberGenerator = new Random();
            _playerOneState = new PlayerState();
            _playerTwoState = new PlayerState();
        }

        public async Task CoinToss(Guid playerOneId, Guid playerTwoId)
        {
            var playerOne = await _unitOfWork.PlayerRepository.FindAsync(playerOneId);
            var playerTwo = await _unitOfWork.PlayerRepository.FindAsync(playerTwoId);

            var result = randomNumberGenerator.CoinFlip(2);

            if (result == 0)
            {
                playerOne.IsAttacker = true;
                playerTwo.IsAttacker = false;
            }
            else
            {
                playerOne.IsAttacker = false;
                playerTwo.IsAttacker = true;
            }
            await _unitOfWork.CommitAsync();
        }
        public async Task<Game> PlayGame(Guid playerOneId, Guid playerTwoId)
        {
            var playerOne = await _unitOfWork.PlayerRepository.FindAsync(playerOneId);
            var playerTwo = await _unitOfWork.PlayerRepository.FindAsync(playerTwoId);

            var game = new Game
            {
                PlayerOne = playerOne,
                PlayerOneStatistics = new PlayByPlayStatistics(),
                PlayerTwo = playerTwo,
                PlayerTwoStatistics = new PlayByPlayStatistics(),
                PlayerOneTotalScore = 0,
                PlayerTwoTotalScore = 0,
            };

            while (game.PlayerOneTotalScore < EndGameScore && game.PlayerTwoTotalScore < EndGameScore)
            {
                if (game.PlayerOne.IsAttacker ?? true)
                {
                    var playerOneScorePointAmount = randomNumberGenerator.Randomize(1, 3);
                    game.PlayerOneStatistics.ScorePoint = playerOneScorePointAmount;
                    if (playerOneScorePointAmount == 1)
                    {
                        var playerOneAttemptToScoreOne = 
                            _playerOneState.ScoringAttempt(game.PlayerOne.InsideScoring, game.PlayerTwo.Defending, playerOneScorePointAmount);
                       
                        ShotAttempt(playerOneAttemptToScoreOne, game);
                    }
                    else
                    {
                        var playerOneAttemptToScoreTwo = 
                            _playerOneState.ScoringAttempt(game.PlayerOne.OutsideScoring, game.PlayerTwo.Defending, playerOneScorePointAmount);
                        
                        ShotAttempt(playerOneAttemptToScoreTwo, game);
                    }
                }
                else
                {
                    var playerTwoScorePointAmount = randomNumberGenerator.Randomize(1, 3);

                    game.PlayerTwoStatistics.ScorePoint = playerTwoScorePointAmount;
                    if (playerTwoScorePointAmount == 1)
                    {
                        var playerTwoAttemptToScoreOne =
                            _playerTwoState.ScoringAttempt(game.PlayerTwo.InsideScoring, game.PlayerOne.Defending, playerTwoScorePointAmount);
                        
                        ShotAttempt(playerTwoAttemptToScoreOne, game);
                    }
                    else
                    {
                        var playerTwoAttemptToScoreTwo =
                            _playerTwoState.ScoringAttempt(game.PlayerTwo.OutsideScoring, game.PlayerOne.Defending, playerTwoScorePointAmount);
                        
                        ShotAttempt(playerTwoAttemptToScoreTwo, game);
                    }
                }
            }
            return game;
        }
        private void ShotAttempt(byte playerAttemptToScore, Game game)
        {
            if (game.PlayerOne.IsAttacker ?? true)
            {
                if (playerAttemptToScore == 0)
                {
                    _playerTwoState.Rebound(game.PlayerOne.Rebounding, game.PlayerTwo.Rebounding);
                }
                else
                {
                    game.PlayerOne.IsAttacker = false;
                    game.PlayerTwo.IsAttacker = true;
                    game.PlayerOneTotalScore += playerAttemptToScore;
                    return;
                }
            }
            if (game.PlayerTwo.IsAttacker ?? true)
            {
                if (playerAttemptToScore == 0)
                {
                    _playerOneState.Rebound(game.PlayerTwo.Rebounding, game.PlayerOne.Rebounding);
                }
                else
                {
                    game.PlayerOne.IsAttacker = true;
                    game.PlayerTwo.IsAttacker = false;
                    game.PlayerTwoTotalScore += playerAttemptToScore;
                    return;
                }
            }
        }
    }
}