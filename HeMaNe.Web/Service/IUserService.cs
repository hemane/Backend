using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Service
{
    public interface IUserService
    {
        User AuthUser(UserDto user);
        bool ChangePassword(UserDto user, string newPassword);
        User CurrentUser();
        ScopedFilter CurrentScope();
        Group CurrentGroup();
        bool IsAdmin();
        void Edit(UserDto userDto);
    }
}
