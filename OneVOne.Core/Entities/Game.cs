namespace OneVOne.GameService.Core.Entities
{
    public class Game : EntityId
    {
        public virtual Player PlayerOne { get; set; }
        public byte PlayerOneTotalScore { get; set; }
        public bool PlayerOneWin { get; set; }
        public bool PlayerTwoWin { get; set; }
        public byte PlayerTwoTotalScore { get; set; }
        public DateTime GameTime { get; set; }
        public virtual Player PlayerTwo { get; set; }

        public Guid UserId { get; set; }

        public virtual PlayByPlayStatistics PlayerOneStatistics { get; set; }
        public virtual PlayByPlayStatistics PlayerTwoStatistics { get; set; }

        public Game Clone()
        {
            var playerOneCopy = PlayerOne.Clone();
            var playerTwoCopy = PlayerTwo.Clone();

            return new Game
            {
                PlayerOne = playerOneCopy,
                PlayerOneTotalScore = PlayerOneTotalScore,
                PlayerOneWin = PlayerOneWin,
                PlayerTwoWin = PlayerTwoWin,
                PlayerTwoTotalScore = PlayerTwoTotalScore,
                GameTime = GameTime,
                PlayerTwo = playerTwoCopy,
                UserId = UserId,
                PlayerOneStatistics = PlayerOneStatistics,
                PlayerTwoStatistics = PlayerTwoStatistics
            };
        }
    }
}
