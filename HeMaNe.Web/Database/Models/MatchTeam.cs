namespace HeMaNe.Web.Database.Models
{
    public class MatchTeam
    {
        public virtual Match Match { get; set; }
        public int MatchId { get; set; }

        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

        public decimal Score { get; set; }
    }
}
