using System;

namespace HeMaNe.Web.Database.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public virtual League League { get; set; }
    }
}
