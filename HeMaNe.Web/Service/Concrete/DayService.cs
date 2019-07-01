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
    public class DayService : IDayService
    {
        private readonly HemaneContext _context;

        public DayService(HemaneContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DayDto>> FindByLeague(int leagueId)
        {
            var days = await _context.Days.Where(d => d.League.Id == leagueId).ToListAsync();
            return days.Select(d => d.AsDto());
        }

        public async Task<int> CreateAsync(DayDto dto)
        {
            if (!await this.ValidateDateTime(dto)) return -1;

            var model = dto.AsModel(this._context);
            model.Id = 0;
            await this._context.Days.AddAsync(model);
            await this._context.SaveChangesAsync();
            return model.Id;
        }

        public async Task EditAsync(DayDto dto)
        {
            if (!await this.ValidateDateTime(dto)) return;
            var model = await this.FindSingleDayAsync(dto.Id);
            dto.MapTo(model, this._context);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this._context.Days.Remove(await this.FindSingleDayAsync(id));
            await this._context.SaveChangesAsync();
        }

        private Task<Day> FindSingleDayAsync(int id)
        {
            return this._context.Days.SingleAsync(d => d.Id == id);
        }

        private async Task<bool> ValidateDateTime(DayDto dto)
        {
            var day = await this._context.Days.FirstOrDefaultAsync(d =>
                d.DateTimeOffset == dto.DateTimeOffset && d.League.Id == dto.LeagueId);
            return day == null;
        }

        private async Task<bool> ValidateDateTime(League league, DateTimeOffset dateTimeOffset)
        {
            var day = await this._context.Days.FirstOrDefaultAsync(d =>
                d.DateTimeOffset == dateTimeOffset && d.League.Id == league.Id);
            return day == null;
        }
    }
}
