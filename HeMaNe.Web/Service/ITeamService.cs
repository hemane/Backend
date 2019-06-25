using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Service
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAsync(ScopedFilter filter);
        Task<TeamDto> GetAsync(int id);
        Task SaveAsync(TeamDto team);
        Task DeleteAsync(int id);
        Task<bool> HasAccess(TeamDto teamDto);
        Task<bool> HasAccess(int id);
    }
}