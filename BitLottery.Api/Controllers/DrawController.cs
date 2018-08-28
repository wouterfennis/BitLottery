using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitLottery.Api.Models;
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
    public async Task<int> GenerateDraw(DrawConfiguration drawConfiguration)
    {
      Draw draw = await _lottery.GenerateDrawAsync(drawConfiguration.DrawDate, drawConfiguration.NumberOfBallots);
      int drawId = _drawRepository.Insert(draw);
      return drawId;
    }

    [HttpGet("{id}")]
    public Draw GetDraw(int id)
    {
      Draw draw = _drawRepository.Get(id);
      return draw;
    }
  }
}
