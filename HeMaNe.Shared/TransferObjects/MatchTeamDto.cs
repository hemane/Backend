namespace HeMaNe.Shared.TransferObjects
{
    public class MatchTeamDto
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string ClubName { get; set; }
        public string TeamName { get; set; }
        public decimal Score { get; set; }
    }
}