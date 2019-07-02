namespace HeMaNe.Shared.TransferObjects
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueLabel { get; set; }
        public string ClubLabel { get; set; }
    }
}