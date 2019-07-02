using System.Linq;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HeMaNe.Web.Extensions
{
    public static class LeagueExtensions
    {
        public static LeagueDto MapToLeagueDto(this League league)
        {
            return new LeagueDto 
            {
                Id = league.Id,
                Name = league.Name,
                SportId = league.Sport.Id,
                SportLabel = league.Sport.Name
            };
        }

        public static void WrapIn(this LeagueDto dto, League league, HemaneContext context)
        {
            league.Name = dto.Name;
            league.Sport = context.Sports.Single(s => s.Id == dto.SportId);
        }
    }
}