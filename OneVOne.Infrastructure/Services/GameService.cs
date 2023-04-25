using OneVOne.Common;
using OneVOne.GameService.Core.Entities;
using OneVOne.GameService.Infrastructure.GameState;
using OneVOne.GameService.Infrastructure.Services.Interfaces;

namespace OneVOne.GameService.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Random randomNumberGenerator;
        private const byte EndGameScore = 11;
        private const byte OnePoint = 1;
        private const byte TwoPoints = 2;


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
        public async Task<Game> PlayGame(Guid playerOneId, Guid playerTwoId, string gameTime)
        {
            var playerOne = await _unitOfWork.PlayerRepository.FindAsync(playerOneId);
            var playerTwo = await _unitOfWork.PlayerRepository.FindAsync(playerTwoId);

            var game = new Game
            {
                GameTime = DateTime.Parse(gameTime),
                PlayerOne = playerOne,
                PlayerOneStatistics = new PlayByPlayStatistics(),
                PlayerTwo = playerTwo,
                PlayerTwoStatistics = new PlayByPlayStatistics(),
                PlayerOneTotalScore = 0,
                PlayerTwoTotalScore = 0,
            };
            var gameClone = game.Clone();
            var roundsCount = 0;
            while (gameClone.PlayerOneTotalScore < EndGameScore && gameClone.PlayerTwoTotalScore < EndGameScore)
            {
                roundsCount++;
                _playerOneState.Fatigue(gameClone.PlayerOne, roundsCount);
                _playerOneState.Fatigue(gameClone.PlayerTwo, roundsCount);
                if (gameClone.PlayerOne.IsAttacker)
                {
                    if (randomNumberGenerator.Randomize(1, 3) == OnePoint)
                    {
                        game.PlayerOneStatistics.ScorePoint = OnePoint;
                        ShotAttempt(_playerOneState.ScoringAttempt(gameClone.PlayerOne.InsideScoring, gameClone.PlayerTwo.Defending, OnePoint), gameClone);
                        Console.WriteLine($"playerOne.ScorePoint: {game.PlayerOneStatistics.ScorePoint}");
                    }
                    else
                    {
                        game.PlayerOneStatistics.ScorePoint = TwoPoints;
                        ShotAttempt(_playerOneState.ScoringAttempt(gameClone.PlayerOne.OutsideScoring, gameClone.PlayerTwo.Defending, TwoPoints), gameClone);
                        Console.WriteLine($"playerOne.ScorePoint: {game.PlayerOneStatistics.ScorePoint}");

                    }
                }
                else
                {
                    if (randomNumberGenerator.Randomize(1, 3) == OnePoint)
                    {
                        game.PlayerTwoStatistics.ScorePoint = OnePoint;
                        ShotAttempt(_playerTwoState.ScoringAttempt(gameClone.PlayerTwo.InsideScoring, gameClone.PlayerOne.Defending, OnePoint), gameClone);
                        Console.WriteLine($"playerTwo.ScorePoint: {gameClone.PlayerTwoStatistics.ScorePoint}");

                    }
                    else
                    {
                        game.PlayerTwoStatistics.ScorePoint = TwoPoints;
                        ShotAttempt(_playerTwoState.ScoringAttempt(gameClone.PlayerTwo.OutsideScoring, gameClone.PlayerOne.Defending, TwoPoints), gameClone);
                        Console.WriteLine($"playerTwo.ScorePoint: {gameClone.PlayerTwoStatistics.ScorePoint}");

                    }
                }
            }
            Console.WriteLine(roundsCount);
            game.PlayerOneTotalScore = gameClone.PlayerOneTotalScore;
            game.PlayerTwoTotalScore = gameClone.PlayerTwoTotalScore;
            Console.WriteLine();
            Console.WriteLine($"p1Stats:{gameClone.PlayerOne.InsideScoring}, p2Stats: {gameClone.PlayerTwo.InsideScoring}");
            await _unitOfWork.GameRepository.AddAsync(game);
            await _unitOfWork.CommitAsync();

            return game;
        }

        public async Task<Game> GetGame(string gameTime)
        {
            return await _unitOfWork.GameRepository.FindAsync(g => g.GameTime == DateTime.Parse(gameTime));
        }

       

        private void ShotAttempt(byte playerAttemptToScore, Game game)
        {
            if (game.PlayerOne.IsAttacker)
            {
                if (playerAttemptToScore == 0)
                {
                    game.PlayerOne.IsAttacker = 
                        _playerOneState.Rebound(game.PlayerOne.Rebounding, game.PlayerTwo.Rebounding);
                    if (game.PlayerOne.IsAttacker)
                    {
                        game.PlayerTwo.IsAttacker = false;
                    }
                    else
                    {
                        game.PlayerTwo.IsAttacker = true;
                    }
                    return;
                }
                else
                {
                    game.PlayerOne.IsAttacker = false;
                    game.PlayerTwo.IsAttacker = true;
                    game.PlayerOneTotalScore += playerAttemptToScore;
                    return;
                }
            }
            if (game.PlayerTwo.IsAttacker)
            {
                if (playerAttemptToScore == 0)
                {
                    game.PlayerTwo.IsAttacker = 
                        _playerTwoState.Rebound(game.PlayerTwo.Rebounding, game.PlayerOne.Rebounding);
                    if (game.PlayerTwo.IsAttacker)
                    {
                        game.PlayerOne.IsAttacker = false;
                    }
                    else
                    {
                        game.PlayerOne.IsAttacker = true;
                    }
                    return;
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