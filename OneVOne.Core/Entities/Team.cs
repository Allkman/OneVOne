namespace OneVOne.GameService.Core.Entities
{
    public class Team : EntityId
    {
        public string TeamName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Abbreviation { get; set; }
        public virtual ICollection<Player> TeamPlayers { get; set; }
    }
}
