using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.Internal;

namespace HeMaNe.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IUserService _userService;

        public MatchesController(IMatchService matchService, IUserService userService)
        {
            _matchService = matchService;
            _userService = userService;
        }

        [HttpGet("day/{day}")]
        public async Task<IActionResult> MatchesByDay(int day)
        {
            return Ok(await this._matchService.FindByDayAsync(day));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchDto dto)
        {
            if (!this._userService.IsAdmin())
            {
                return Forbid();
            }

            var id = await this._matchService.CreateAsync(dto);
            if (id < 1)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this._userService.IsAdmin())
            {
                return Forbid();
            }

            await this._matchService.DeleteAsync(id);
            return Ok();
        }

    }
}