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

        [HttpGet("getAllCSAT")]
        public async Task<ActionResult<List<CSAT>>> GetAllCSAT([FromQuery]string score="",
                                                 [FromQuery] string fcr="",
                                                 [FromQuery] string email="")
        {
                List<CSAT> csats = await _csatService.ListAllCSAT(score,
                                                                  fcr,
                                                                  email);

                return Ok(csats);
        }

        [HttpPost("updateCSATFCR")]
        public async Task<IActionResult> UpdateFCR([FromQuery] string id,
                                                   [FromBody] string fcr)
        {
            var csat = await _csatService.GetCSATById(id);

            if (csat is null)
            {
                return NotFound();
            }

            csat.ProblemSolved = bool.Parse(fcr);

            _csatService.UpdateProblemSolved(id, csat);

            return NoContent();
        }

        [HttpPost("updateCSATComment")]
        public async Task<IActionResult> UpdateComment([FromQuery] string id,
                                                   [FromBody] string comment)
        {
            var csat = await _csatService.GetCSATById(id);

            if (csat is null)
            {
                return NotFound();
            }

            csat.Comment = comment;

            _csatService.UpdateProblemSolved(id, csat);

            return NoContent();
        }

        [HttpPost("getCSATSummary/{email}")]
        public async Task<ActionResult<CSATSummaryByEmail>> GetCSATSummary(
                                                       string email,
                                                       [FromBody] string date)
        {

            List<CSAT> filteredCsats = await _csatService
                                                    .ListAllCSAT("", "", email);

            if (string.IsNullOrEmpty(date) || filteredCsats == null)
            {
                return NoContent();
            }

            DateTime parsedDate;
            DateTime.TryParse(date, out parsedDate);

            List<CSAT> filteredCsatsByDate = filteredCsats
                                               .Where(csat =>
                                                csat.TimeStamp.Date ==
                                                parsedDate.Date).ToList();

            var summary = await _csatService.FormSummary(filteredCsatsByDate);

            return Ok(summary);
        }
    }
}