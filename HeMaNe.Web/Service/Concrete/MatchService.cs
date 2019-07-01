using System;
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

        private Task<Match> FindSingleMatchAsync(int id)
        {
            return this._context.Matches.SingleAsync(m => m.Id == id);
        }
    }
}
