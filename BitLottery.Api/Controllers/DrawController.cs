using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Controllers;
using BitLottery.Database;
using BitLottery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BitLottery.Api.Controllers
{
    [Route("api/[controller]")]
    public class DrawController : Controller, IDrawController
    {
        private readonly ILottery _lottery;
        private readonly IDrawRepository _drawRepository;
        private readonly IBallotRepository _ballotRepository;

        public DrawController(ILottery lottery, IDrawRepository drawRepository, IBallotRepository ballotRepository)
        {
            _lottery = lottery;
            _drawRepository = drawRepository;
            _ballotRepository = ballotRepository;
        }

        [HttpPost]
        public async Task<int> GenerateDraw(DrawConfiguration drawConfiguration)
        {
            Draw draw = await _lottery.GenerateDrawAsync(drawConfiguration.SellUntilDate, drawConfiguration.NumberOfBallots);
            int drawId = _drawRepository.Insert(draw);
            return drawId;
        }

        [HttpGet("{drawId}")]
        public Draw GetDraw(int drawId)
        {
            Draw draw = _drawRepository.Get(drawId);
            return draw;
        }

        [HttpPut("buyBallot/{drawId}")]
        public async Task<int> SellBallotAsync(int drawId)
        {
            Draw draw = _drawRepository.Get(drawId);
            Ballot soldBallot = await _lottery.SellBallotAsync(draw);
            _ballotRepository.Update(soldBallot, soldBallot.Id);
            return soldBallot.Id;
        }

        [HttpPut("drawWins/{drawId}")]
        public async Task DrawWinsAsync(int drawId)
        {
            Draw draw = _drawRepository.Get(drawId);
            Ballot winningBallot = await _lottery.DrawWinsAsync(draw);
            winningBallot.RegisterAsWinner();
            _ballotRepository.Update(winningBallot, winningBallot.Id);
            _drawRepository.Update(draw, draw.Id);
        }
    }
}
