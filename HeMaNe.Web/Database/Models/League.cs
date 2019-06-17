using System.Collections;
using System.Collections.Generic;

namespace HeMaNe.Web.Database.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Sport Sport { get; set; }
        public virtual ICollection<Day> Days { get; set; }
    }
}
