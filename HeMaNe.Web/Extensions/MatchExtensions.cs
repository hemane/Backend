using System.Linq;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Extensions
{
    public static class MatchExtensions
    {
        public static MatchDto AsDto(this Match match)
        {
            return new MatchDto
            {
                Id = match.Id,
                DayId = match.Day.Id
            };
        }

        public static Match AsModel(this MatchDto dto, HemaneContext context)
        {
            return new Match
            {
                Id = dto.Id,
                Day = FindSingleDay(context, dto.DayId)
            };
        }

        private static Day FindSingleDay(HemaneContext context, int id)
        {
            return context.Days.Single(l => l.Id == id);
        }
    }
}