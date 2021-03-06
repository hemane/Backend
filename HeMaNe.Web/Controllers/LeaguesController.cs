﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeMaNe.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueService _service;

        public LeaguesController(ILeagueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _service.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpGet("sport/{sport}")]
        public async Task<IActionResult> IndexBySport(int sport)
        {
            return Ok(await _service.GetBySportAsync(sport));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] LeagueDto dto)
        {
            if (!this._service.HasAccess(dto))
            {
                return Forbid();
            }
            await this._service.SaveAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this._service.HasAccess(id))
            {
                return Forbid();
            }
            await this._service.DeleteAsync(id);
            return Ok();
        }
    }
}