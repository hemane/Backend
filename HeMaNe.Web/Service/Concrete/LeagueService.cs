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
    internal class LeagueService : ILeagueService
    {
        private readonly HemaneContext _context;
        private readonly IUserService _userService;

        public LeagueService(HemaneContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<LeagueDto>> GetAsync()
        {
            var leagues = await _context.Leagues.ToListAsync();
            return leagues.Select(LeagueExtensions.MapToLeagueDto);
        }

        public async Task<LeagueDto> GetAsync(int id)
        {
            var league = await GetSingleLeagueAsync(id);
            return league.MapToLeagueDto();
        }

        private async Task<League> GetSingleLeagueAsync(int id)
        {
            return await _context.Leagues.SingleAsync(l => l.Id == id);
        }

        public async Task SaveAsync(LeagueDto leagueDto)
        {
            League league;
            if(leagueDto.Id > 0)
            {
                league = await this.GetSingleLeagueAsync(leagueDto.Id);
            }
            else
            {
                league = new League();
                _context.Leagues.Add(league);
            }

            leagueDto.WrapIn(league, _context);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Leagues.Remove(await this.GetSingleLeagueAsync(id));
            await _context.SaveChangesAsync();
        }

        public Task<bool> CanSave(LeagueDto leagueDto)
        {
            return new Task<bool>(() => this._userService.CurrentUser().Group == Group.Administrator);
        }
    }
}