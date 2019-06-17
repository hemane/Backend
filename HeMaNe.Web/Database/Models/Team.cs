using System.Collections.Generic;

namespace HeMaNe.Web.Database.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Club Club { get; set; }
        public virtual League League { get; set; }
        public virtual ICollection<MatchTeam> MatchTeams { get; set; }
    }
}
