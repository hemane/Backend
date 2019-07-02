using System;
using System.Collections.Generic;
using System.Text;

namespace HeMaNe.Shared.TransferObjects
{
    public class DisplayDto
    {
        public IEnumerable<DisplayDayDto> Days { get; set; }
    }

    public class DisplayDayDto
    {
        public DateTime Date { get; set; }
        public IEnumerable<DisplaySportDto> Sports { get; set; }
    }

    public class DisplaySportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DisplayLeagueDto> Leagues { get; set; }
    }

    public class DisplayLeagueDto
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public string Name { get; set; }
        public IEnumerable<DisplayMatchDto> Matches { get; set; }
    }

    public class DisplayMatchDto
    {
        public int Id { get; set; }
        public IEnumerable<DisplayTeamDto> Teams { get; set; }
    }

    public class DisplayTeamDto
    {
        public int Id { get; set; }
        public string Club { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }
    }
}
