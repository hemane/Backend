using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeMaNe.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DisplayController : ControllerBase
    {
        private readonly IDisplayService _displayService;

        public DisplayController(IDisplayService displayService)
        {
            _displayService = displayService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _displayService.DisplayLeaguesAsync());
        }

    }
}