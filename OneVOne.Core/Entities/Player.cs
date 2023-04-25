namespace OneVOne.GameService.Core.Entities
{
    public class Player : EntityId
    {
        private byte? _outsideScoring { get; set; }
        private byte? _insideScoring { get; set; }
        private byte? _defending { get; set; }
        private byte? _playmaking { get; set; }
        private byte? _rebounding { get; set; }
        public string? Position { get; set; }
        public byte? OutsideScoring { get => _outsideScoring; set => _outsideScoring = value; }
        public byte? InsideScoring { get => _insideScoring; set => _insideScoring = value; }
        public byte? Defending { get => _defending; set => _defending = value; }
        public byte? Athleticism { get; set; }
        public byte? Playmaking { get => _playmaking; set => _playmaking = value; }
        public byte? Rebounding { get => _rebounding; set => _rebounding = value; }
        public bool IsAttacker { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? TeamId { get; set; }
        public string? NbaPlayerPageUrl { get; set; }
        public virtual Person Person { get; set; }
        public virtual PlayerImage PlayerImage { get; set; }

        public Player Clone()
        {
            return new Player
            {
                _outsideScoring = _outsideScoring,
                _insideScoring = _insideScoring,
                _defending = _defending,
                _playmaking = _playmaking,
                _rebounding = _rebounding,
                Position = Position,
                Athleticism = Athleticism,
                IsAttacker = IsAttacker,
                PersonId = PersonId,
                TeamId = TeamId,
                NbaPlayerPageUrl = NbaPlayerPageUrl,
                Person = Person,
                PlayerImage = PlayerImage
            };
        }
    }
}
