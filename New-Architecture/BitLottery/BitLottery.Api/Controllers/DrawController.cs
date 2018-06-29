using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitLottery.Business;
using BitLottery.Controllers;
using BitLottery.Database;
using BitLottery.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitLottery.Api.Controllers
{
    [Route("api/[controller]")]
    public class DrawController : Controller, IDrawController
    {
        private readonly ILottery _lottery;
        private readonly IRepository<Draw, int> _drawRepository;

        public DrawController(ILottery lottery, IRepository<Draw, int> drawRepository)
        {
            _lottery = lottery;
            _drawRepository = drawRepository;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateDraw()
        {
            Draw draw = await _lottery.GenerateDrawAsync();
            _drawRepository.Insert(draw);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public string GetDraw(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IEnumerable<string> GetDraws()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
