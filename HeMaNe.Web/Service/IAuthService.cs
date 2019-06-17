using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;

namespace HeMaNe.Web.Service
{
    public interface IAuthService
    {
        AuthDto Authenticate(UserDto userDto);
    }
}
