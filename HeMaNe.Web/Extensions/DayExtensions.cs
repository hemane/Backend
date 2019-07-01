using System.Linq;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HeMaNe.Web.Extensions
{
    public static class DayExtensions
    {
        public static DayDto AsDto(this Day day)
        {
            return new DayDto
            {
                Id = day.Id,
                DateTimeOffset = day.DateTimeOffset,
                LeagueId = day.League.Id
            };
        }

        public static Day AsModel(this DayDto dto, HemaneContext context)
        {
            return new Day
            {
                Id = dto.Id,
                League = FindSingleLeague(context, dto.LeagueId),
                DateTimeOffset = dto.DateTimeOffset
            };
        }

        public static void MapTo(this DayDto dto, Day model, HemaneContext context)
        {
            model.DateTimeOffset = dto.DateTimeOffset;
            model.League = FindSingleLeague(context, dto.LeagueId);
        }

        private static League FindSingleLeague(HemaneContext context, int id)
        {
            return context.Leagues.Single(l => l.Id == id);
        }
    }
}