using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HeMaNe.Web.Extensions
{
    public static class TeamExtensions
    {
        public static TeamDto MapToTeamDto(this Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                ClubId = team.Club.Id,
                ClubLabel = team.Club.Name,
                LeagueId = team.League.Id,
                LeagueLabel = $"{team.League.Sport.Name}/{team.League.Name}",
                Name = team.Name
            };
        }

        public static async Task WrapInAsync(this TeamDto dto, Team team, HemaneContext context)
        {
            team.Name = dto.Name;
            team.Club = await context.Clubs.SingleAsync(c => c.Id == dto.ClubId);
            team.League = await context.Leagues.SingleAsync(l => l.Id == dto.LeagueId);
        }
    }
}