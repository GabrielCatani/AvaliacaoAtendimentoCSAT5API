using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AvaliacaoAtendimentoCSAT5API.Models;
using AvaliacaoAtendimentoCSAT5API.Services;

namespace AvaliacaoAtendimentoCSAT5API.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class CSATController : ControllerBase
    {
        private readonly CSATService _csatService;

        public CSATController(CSATService csatService)
        {
            _csatService = csatService;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }

        [HttpPost("createCSAT")]
        public async Task<IActionResult> PostCSAT(CSAT newCSAT)
        {
            if (newCSAT.Score < 1 || newCSAT.Score > 5) {
                return BadRequest("Invalid CSAT Score");
            }

            //persist CSAT
            await _csatService.CreateAsync(newCSAT);
            
            return Ok(newCSAT.Id);
        }
    }
}

