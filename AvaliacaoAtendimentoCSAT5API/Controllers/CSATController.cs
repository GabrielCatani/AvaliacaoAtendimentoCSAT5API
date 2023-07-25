using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public void IndexReceive(string id)
        {
            Console.WriteLine(id);
        }
    }
}

