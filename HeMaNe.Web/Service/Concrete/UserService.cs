using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeMaNe.Web.Service.Concrete
{
    internal class UserService : IUserService
    {
        private readonly HemaneContext _hemaneContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HemaneContext hemaneContext, IHttpContextAccessor httpContextAccessor)
        {
            this._hemaneContext = hemaneContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public User AuthUser(UserDto userDto)
        {
            ComputeHash(userDto);

            return this._hemaneContext.Users.FirstOrDefault(u =>
                u.Username == userDto.Username && u.Password == userDto.Password);
        }

        public bool ChangePassword(UserDto userDto, string newPassword)
        {
            var user = this.AuthUser(userDto);
            if (user == null) return false;

            userDto.Password = newPassword;
            ComputeHash(userDto);

            user.Password = userDto.Password;
            this._hemaneContext.SaveChanges();
            return true;
        }

        public User CurrentUser()
        {
            return _hemaneContext.Users.Single(u =>
                u.Id == int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name));
        }

        private static void ComputeHash(UserDto user)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                user.Password = builder.ToString();
            }
        }
    }
}