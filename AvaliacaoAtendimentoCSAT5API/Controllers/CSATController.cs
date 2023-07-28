﻿using System;
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
        private readonly ICSATService _csatService;

        public CSATController(ICSATService csatService)
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
            await _csatService.CreateCSAT(newCSAT);
            
            return Ok(newCSAT.Id);
        }

        [HttpGet("getCSAT")]
        public async Task<ActionResult<CSAT>> GetCSAT([FromQuery]string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 36)
            {
                return BadRequest();
            }

            var csat = await _csatService.GetCSATById(id);

            if (csat is null)
            {
                return NotFound();
            }

            return Ok(csat);
        }
    }
}

