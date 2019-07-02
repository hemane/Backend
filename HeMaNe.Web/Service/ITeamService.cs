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
        bool HasAccess(TeamDto teamDto);
        bool HasAccess(int id);
        Task<IEnumerable<TeamDto>> GetByClubAsync(ScopedFilter currentScope, int clubId);
        Task<IEnumerable<TeamDto>> GetByLeagueAsync(ScopedFilter currentScope, int leagueId);
        Task<IEnumerable<TeamDto>> GetForMatchAsync(ScopedFilter currentScope, int match);
    }
}