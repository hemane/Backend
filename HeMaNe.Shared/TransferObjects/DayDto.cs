using System;
using System.Collections.Generic;
using System.Text;

namespace HeMaNe.Shared.TransferObjects
{
    public class DayDto
    {
        public int Id { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public int LeagueId { get; set; }
    }
}
