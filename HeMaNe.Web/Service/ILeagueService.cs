using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Service
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDto>> GetAsync();
        Task<LeagueDto> GetAsync(int id);
        Task SaveAsync(LeagueDto league);
        Task DeleteAsync(int id);
        Task<bool> HasAccess(LeagueDto leagueDto);
        Task<bool> HasAccess(int id);
    }
}