using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace HeMaNe.Web.Service.Concrete
{
    public class MatchService : IMatchService
    {
        private readonly HemaneContext _context;

        public MatchService(HemaneContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatchDto>> FindByDayAsync(int day)
        {
            var matches = await _context.Matches.Where(m => m.Day.Id == day).ToListAsync();
            return matches.Select(m => m.AsDto());
        }

        public async Task<int> CreateAsync(MatchDto dto)
        {
            var model = dto.AsModel(this._context);
            await this._context.Matches.AddAsync(model);
            await this._context.SaveChangesAsync();
            return model.Id;
        }

        public async Task DeleteAsync(int id)
        {
            this._context.Matches.Remove(await this.FindSingleMatchAsync(id));
            await this._context.SaveChangesAsync();
        }

        public async Task<MatchDto> FindById(int id)
        {
            var match = await this._context.Matches.SingleAsync(m => m.Id == id);
            return match.AsDto();
        }

        public async Task<IEnumerable<MatchTeamDto>> FindMatchTeams(int matchId)
        {
            var l = await this._context.MatchTeams.Where(mt => mt.MatchId == matchId).ToListAsync();
            return l.Select(l1 => l1.AsDto());
        }

        public async Task AddMatchTeam(MatchTeamDto dto)
        {
            var match = await this.FindSingleMatchAsync(dto.MatchId);
            if (match.Teams.Any(t => t.TeamId == dto.TeamId)) return;
            var mt = new MatchTeam
            {
                MatchId = dto.MatchId,
                TeamId = dto.TeamId,
                Score = 0
            };
            await this._context.MatchTeams.AddAsync(mt);
            await this._context.SaveChangesAsync();
        }

        public async Task RemoveMatchTeam(MatchTeamDto dto)
        {
            this._context.MatchTeams.Remove(await this.FindMatchTeam(dto));
            await this._context.SaveChangesAsync();
        }

        public async Task SetMatchTeamScore(MatchTeamDto dto)
        {
            var tm = await this.FindMatchTeam(dto);
            tm.Score = dto.Score;
            await this._context.SaveChangesAsync();
        }

        private async Task<MatchTeam> FindMatchTeam(MatchTeamDto dto)
        {
            return (await this.FindSingleMatchAsync(dto.MatchId)).Teams.Single(t => t.TeamId == dto.TeamId);
        }


        private Task<Match> FindSingleMatchAsync(int id)
        {
            return this._context.Matches.SingleAsync(m => m.Id == id);
        }
    }
}
