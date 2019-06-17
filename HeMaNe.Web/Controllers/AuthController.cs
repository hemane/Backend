using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            return Ok();
        }
    }
}