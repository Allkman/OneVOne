namespace OneVOne.GameService.Core.Entities
{
    public class PlayerImage : EntityId
    {
        public byte[]? Image { get; set; }
        public Guid? PlayerId { get; set; }
    }
}
