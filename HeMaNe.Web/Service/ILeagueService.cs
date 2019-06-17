using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDto>> GetAsync();
        Task<LeagueDto> GetAsync(int id);
        Task SaveAsync(LeagueDto league);
        Task DeleteAsync(int id);
    }
}