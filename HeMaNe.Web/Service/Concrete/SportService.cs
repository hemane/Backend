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
    internal class SportService : ISportService
    {
        private readonly HemaneContext _context;
        private readonly IUserService _userService;

        public SportService(HemaneContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        public async Task<IEnumerable<SportDto>> GetAsync()
        {
            var sports = await _context.Sports.ToListAsync();
            return sports.Select(SportExtensions.MapToSportDto);
        }

        public async Task<SportDto> GetAsync(int id)
        {
            var sport = await this.GetSingleSportById(id);
            return sport.MapToSportDto();
        }

        private async Task<Sport> GetSingleSportById(int id)
        {
            return await _context.Sports.SingleAsync(s => s.Id == id);
        }

        public async Task SaveAsync(SportDto dto)
        {
            Sport sport;
            if (dto.Id > 0)
            {
                sport = await this.GetSingleSportById(dto.Id);
            }
            else
            {
                sport = new Sport();
                _context.Sports.Add(sport);
            }

            dto.WrapIn(sport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Sports.Remove(await this.GetSingleSportById(id));
            await _context.SaveChangesAsync();
        }

        public bool HasAccess(SportDto sportDto)
        {
            return this._userService.IsAdmin();
        }

        public bool HasAccess(int id)
        {
            var obj = this.GetAsync(id).Result;
            return this.HasAccess(obj);
        }
    }
}