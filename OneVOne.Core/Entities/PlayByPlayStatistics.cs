namespace OneVOne.GameService.Core.Entities
{
    public class PlayByPlayStatistics : EntityId
    {
        public Guid PlayerId { get; set; }
        public byte ScorePoint { get; set; }
        public byte Steal { get; set; }
        public byte Rebound { get; set; }
        public byte Block { get; set; }
        public byte Foul { get; set; }
    }
}
