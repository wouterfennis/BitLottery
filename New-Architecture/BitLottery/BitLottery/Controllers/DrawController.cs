using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BitLottery.Controllers
{
    [Route("api/[controller]")]
    public class DrawController : Controller, IDrawController
    {
        //</inheritdoc>
        [HttpGet]
        public IEnumerable<string> GetDraws()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string GetDraw(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void GenerateDraw()
        {
        }
    }
}
