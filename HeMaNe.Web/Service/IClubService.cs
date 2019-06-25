using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Service
{
    public interface IClubService
    {
        Task<IEnumerable<ClubDto>> GetAsync(ScopedFilter filter);
        Task<ClubDto> GetAsync(int id);
        Task SaveAsync(ClubDto club);
        Task DeleteAsync(int id);
        Task<bool> HasAccess(ClubDto clubDto);
        Task<bool> HasAccess(int id);
    }
}