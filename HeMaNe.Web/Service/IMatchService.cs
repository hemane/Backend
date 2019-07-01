using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchDto>> FindByDayAsync(int day);
        Task<int> CreateAsync(MatchDto dto);
        Task DeleteAsync(int id);
    }
}
