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
    internal class ClubService : IClubService
    {
        private readonly HemaneContext _context;
        private readonly IUserService _userService;

        public ClubService(HemaneContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<ClubDto>> GetAsync(ScopedFilter filter)
        {
            var clubs = await _context.Clubs.Where(c => filter == ScopedFilter.All || c.Manager == this._userService.CurrentUser()).ToListAsync();
            return clubs.Select(ClubExtensions.MapToClubDto);
        }

        public async Task<ClubDto> GetAsync(int id)
        {
            var club = await this.GetSingleClubAsync(id);
            return club.MapToClubDto();
        }

        private Task<Club> GetSingleClubAsync(int id)
        {
            return _context.Clubs.SingleAsync(c => c.Id == id && c.Manager == this._userService.CurrentUser());
        }

        public async Task SaveAsync(ClubDto clubDto)
        {
            Club club;
            if (clubDto.Id > 0)
            {
                club = await this.GetSingleClubAsync(clubDto.Id);
            }
            else
            {
                club = new Club();
                await _context.Clubs.AddAsync(club);
            }

            club.Name = clubDto.Name;
            club.City = clubDto.City;
            club.Postcode = clubDto.Postcode;
            club.Manager = this._userService.CurrentUser();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Clubs.Remove(await this.GetSingleClubAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanSave(ClubDto clubDto)
        {
            // Neuer Club
            if (clubDto.Id == 0)
            {
                return true;
            }

            // Administrator
            var user = this._userService.CurrentUser();
            if (user.Group == Group.Administrator)
            {
                return true;
            }

            // Manger vom Club
            var club = await this.GetSingleClubAsync(clubDto.Id);
            return club.Manager.Id == user.Id;
        }
    }
}