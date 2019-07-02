using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HeMaNe.Web.Service.Concrete
{
    internal class TeamService : ITeamService
    {
        private readonly HemaneContext _context;
        private readonly IUserService _userService;
        private readonly IClubService _clubService;
        private readonly IMatchService _matchService;

        public TeamService(HemaneContext context, IUserService userService, IClubService clubService, IMatchService matchService)
        {
            _context = context;
            _userService = userService;
            _clubService = clubService;
            _matchService = matchService;
        }

        public async Task<IEnumerable<TeamDto>> GetAsync(ScopedFilter filter)
        {
            var teams = await this.ScopedTeams(filter).ToListAsync();
            return teams.Select(TeamExtensions.MapToTeamDto);
        }

        public async Task<TeamDto> GetAsync(int id)
        {
            var team = await this.GetTeamAsync(id);
            return team.MapToTeamDto();
        }

        private async Task<Team> GetTeamAsync(int id)
        {
            return await _context.Teams.SingleAsync(t => t.Id == id);
        }

        public async Task SaveAsync(TeamDto dto)
        {
            Team team;
            if (dto.Id > 1)
            {
                team = await this.GetTeamAsync(dto.Id);
            }
            else
            {
                team = new Team();
                _context.Teams.Add(team);
            }

            await dto.WrapInAsync(team, this._context);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this._context.Teams.Remove(await this.GetTeamAsync(id));
            await this._context.SaveChangesAsync();
        }

        public bool HasAccess(TeamDto teamDto)
        {
            // Administrator
            var user = this._userService.CurrentUser();
            if (user.Group == Group.Administrator)
            {
                return true;
            }

            // Manger vom Club hinter dem Team
            var club = this._clubService.GetAsync(teamDto.ClubId).Result;
            return club.ManagerId == user.Id;
        }

        public bool HasAccess(int id)
        {
            var obj = this.GetAsync(id).Result;
            return this.HasAccess(obj);
        }

        public async Task<IEnumerable<TeamDto>> GetByClubAsync(ScopedFilter filter, int clubId)
        {
            var clubs = await this._context.Teams.Where(t => t.Club.Id == clubId).ToListAsync();
            return clubs.Select(c => c.MapToTeamDto());
        }

        public async Task<IEnumerable<TeamDto>> GetByLeagueAsync(ScopedFilter filter, int leagueId)
        {
            var clubs = await this._context.Teams.Where(t => t.League.Id == leagueId).ToListAsync();
            return clubs.Select(c => c.MapToTeamDto());
        }

        public async Task<IEnumerable<TeamDto>> GetForMatchAsync(ScopedFilter filter, int match)
        {
            var mo = (await _matchService.FindById(match)).AsModel(this._context);
            var clubs = await this._context.Teams.Where(t => t.MatchTeams.All(m => m.MatchId != match) && mo.Day.League.Id == t.League.Id).ToListAsync();
            return clubs.Select(c => c.MapToTeamDto());
        }

        private IQueryable<Team> ScopedTeams(ScopedFilter filter)
        {
            IQueryable<Team> teams = this._context.Teams;
            if (filter == ScopedFilter.Manager)
            {
                var userId = this._userService.CurrentUser().Id;
                teams = teams.Where(c => c.Club.Manager.Id == userId);
            }
            return teams;
        }
    }
}