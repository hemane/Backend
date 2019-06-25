using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAsync(ScopedFilter filter);
        Task<TeamDto> GetAsync(int id);
        Task SaveAsync(TeamDto team);
        Task DeleteAsync(int id);
    }
}