using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface ISportService
    {
        Task<IEnumerable<SportDto>> GetAsync();
        Task<SportDto> GetAsync(int id);
        Task SaveAsync(SportDto sport);
        Task DeleteAsync(int id);
    }
}
