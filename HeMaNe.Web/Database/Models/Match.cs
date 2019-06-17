using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeMaNe.Web.Database.Models
{
    public class Match
    {
        public int Id { get; set; }
        public virtual ICollection<MatchTeam> Teams { get; set; }
        public virtual Day Day { get; set; }
    }
}
