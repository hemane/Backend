using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database;
using HeMaNe.Web.Database.Models;
using Microsoft.AspNetCore.Http;
using Group = HeMaNe.Web.Database.Models.Group;

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
            var user = this._hemaneContext.Users.FirstOrDefault(u =>
                u.Username == userDto.Username);
            if (user == null)
            {
                return null;
            }

            userDto.Id = user.Id;
            ComputeHash(userDto);
            return user.Password == userDto.Password ? user : null;
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

        public ScopedFilter CurrentScope()
        {
            return this.CurrentUser().Group == Group.Administrator ? ScopedFilter.All : ScopedFilter.Manager;
        }

        public Group CurrentGroup()
        {
            return this.CurrentUser().Group;
        }

        public bool IsAdmin()
        {
            return this.CurrentGroup() == Group.Administrator;
        }

        public void Edit(UserDto userDto)
        {
            if (this.CheckInputPassword(userDto.Password))
            {
                throw new Exception("Passwort wurde vor dem versenden an den Server nicht gehashed!");
            }

            var user = this._hemaneContext.Users.FirstOrDefault(u => u.Username == userDto.Username);
            if (user == null) return;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                ComputeHash(userDto);
                user.Password = userDto.Password;
            }

            this._hemaneContext.SaveChanges();
        }

        private readonly Regex _passwordRegex = new Regex("[0-9a-f]{64}");

        private bool CheckInputPassword(string password)
        {
            return _passwordRegex.IsMatch(password);
        }

        private static void ComputeHash(UserDto user)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes($"{user.Id};{user.Password}"));
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