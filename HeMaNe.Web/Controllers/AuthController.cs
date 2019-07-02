using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Extensions;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeMaNe.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            var auth = this._authService.Authenticate(userDto);
            if (auth == null) return this.Unauthorized();
            return this.Ok(auth);
        }

        [HttpGet]
        public IActionResult Info()
        {
            return Ok(this._userService.CurrentUser().MapToUserDto());
        }

        [HttpPut]
        public IActionResult Edit(UserDto userDto)
        {
            var currentUser = _userService.CurrentUser();
            if (userDto.Username != currentUser.Username && currentUser.Group != Group.Administrator)
            {
                return Forbid();
            }

            this._userService.Edit(userDto);

            return Ok();
        }
    }
}