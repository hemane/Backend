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

        public TeamService(HemaneContext context, IUserService userService, IClubService clubService)
        {
            _context = context;
            _userService = userService;
            _clubService = clubService;
        }

        public async Task<IEnumerable<TeamDto>> GetAsync(ScopedFilter filter)
        {
            var teams = await _context.Teams.ToListAsync();
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
                _context.Teams.Add(new Team());
            }

            await dto.WrapInAsync(team, this._context);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this._context.Teams.Remove(await this.GetTeamAsync(id));
            await this._context.SaveChangesAsync();
        }

        public async Task<bool> HasAccess(TeamDto teamDto)
        {
            // Administrator
            var user = this._userService.CurrentUser();
            if (user.Group == Group.Administrator)
            {
                return true;
            }

            // Manger vom Club hinter dem Team
            var club = await this._clubService.GetAsync(teamDto.ClubId);
            return club.ManagerId == user.Id;
        }

        public async Task<bool> HasAccess(int id)
        {
            var obj = await this.GetAsync(id);
            return await this.HasAccess(obj);
        }
    }
}