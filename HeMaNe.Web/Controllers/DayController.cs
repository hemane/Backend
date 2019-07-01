using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace HeMaNe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDayService _dayService;
        private readonly IUserService _userService;

        public DayController(IDayService dayService, IUserService userService)
        {
            _dayService = dayService;
            _userService = userService;
        }

        [HttpGet("{league}")]
        public async Task<IActionResult> FindByLeague(int league)
        {
            return Ok(await this._dayService.FindByLeague(league));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DayDto dto)
        {
            if (!this._userService.IsAdmin())
            {
                return Forbid();
            }

            var id = await this._dayService.CreateAsync(dto);
            if (id < 1)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] DayDto dto)
        {
            if (!this._userService.IsAdmin())
            {
                return Forbid();
            }

            await this._dayService.EditAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this._userService.IsAdmin())
            {
                return Forbid();
            }

            await this._dayService.DeleteAsync(id);

            return Ok();
        }

    }
}