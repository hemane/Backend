using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;

namespace HeMaNe.Web.Extensions
{
    public static class UserExtensions
    {
        public static UserDto MapToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = string.Empty,
                Group = (int)user.Group
            };
        }
    }
}
