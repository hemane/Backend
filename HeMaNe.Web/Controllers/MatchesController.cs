using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Single(int id)
        {
            return Ok(await this._matchService.FindById(id));
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

        [HttpGet("{id}/teams")]
        public async Task<IActionResult> GetTeams(int id)
        {
            return Ok(await this._matchService.FindMatchTeams(id));
        }

        [HttpPost("teams")]
        public async Task<IActionResult> CreateTeam([FromBody] MatchTeamDto dto)
        {
            await this._matchService.AddMatchTeam(dto);
            return Ok();
        }

        [HttpDelete("teams")]
        public async Task<IActionResult> DeleteTeam([FromBody] MatchTeamDto dto)
        {
            await this._matchService.RemoveMatchTeam(dto);
            return Ok();
        }

        [HttpPut("teams")]
        public async Task<IActionResult> UpdateTeam([FromBody] MatchTeamDto dto)
        {
            await this._matchService.SetMatchTeamScore(dto);
            return Ok();
        }
    }
}