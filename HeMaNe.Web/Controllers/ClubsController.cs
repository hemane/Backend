using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Database.Models;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeMaNe.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ClubsController : ControllerBase
    {
        private readonly IClubService _service;
        private readonly IUserService _userService;

        public ClubsController(IClubService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _service.GetAsync(this._userService.CurrentScope()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            if (!await this._service.HasAccess(id))
            {
                return Forbid();
            }
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ClubDto dto)
        {
            if (!await this._service.HasAccess(dto))
            {
                return Forbid();
            }
            await this._service.SaveAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this._service.HasAccess(id))
            {
                return Forbid();
            }
            await this._service.DeleteAsync(id);
            return Ok();
        }
    }
}