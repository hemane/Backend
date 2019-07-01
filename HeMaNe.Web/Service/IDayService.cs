using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface IDayService
    {
        Task<IEnumerable<DayDto>> FindByLeague(int leagueId);
        Task<int> CreateAsync(DayDto dto);
        Task EditAsync(DayDto dto);
        Task DeleteAsync(int id);
    }
}
