using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface IDisplayService
    {
        Task<DisplayDto> DisplayLeaguesAsync();
    }
}