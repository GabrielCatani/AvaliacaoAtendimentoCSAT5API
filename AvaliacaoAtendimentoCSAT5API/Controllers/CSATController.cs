using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AvaliacaoAtendimentoCSAT5API.Models;

namespace AvaliacaoAtendimentoCSAT5API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CSATController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }

        [HttpPost("/createCSAT")]
        public IActionResult PostCSAT(CSAT newCSAT)
        {
            if (newCSAT.Score < 1 || newCSAT.Score > 5) {
                return BadRequest("Invalid CSAT Score");
            }

            //persist CSAT
            
            return Ok(newCSAT.Id);
        }
    }
}

