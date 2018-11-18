using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Controllers.Interfaces;
using BitLottery.Database.Interfaces;
using BitLottery.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.Api.Controllers
{
    [Route("api/[controller]")]
    public class DrawController : Controller, IDrawController
    {
        private readonly ILottery _lottery;
        private readonly IDrawRepository _drawRepository;
        private readonly IBallotRepository _ballotRepository;

        public DrawController(
            ILottery lottery,
            IDrawRepository drawRepository,
            IBallotRepository ballotRepository)
        {
            _lottery = lottery;
            _drawRepository = drawRepository;
            _ballotRepository = ballotRepository;
        }

        [HttpPost]
        public async Task<int> GenerateDraw(DrawConfiguration drawConfiguration)
        {
            IEnumerable<Ballot> ballots = await _lottery.GenerateBallotsAsync(drawConfiguration.NumberOfBallots);

            var draw = new Draw
            {
                SellUntilDate = drawConfiguration.SellUntilDate,
                Ballots = ballots
            };

            draw.AddPrice(PriceType.Main, drawConfiguration.MainPriceAmount);
            draw.AddPrice(PriceType.FinalDigit, drawConfiguration.FinalNumberPriceAmount);

            int drawNumber = _drawRepository.Insert(draw);
            return drawNumber;
        }

        [HttpGet("{drawNumber}")]
        public Draw GetDraw(int drawNumber)
        {
            Draw draw = _drawRepository.Get(drawNumber);
            return draw;
        }

        [HttpPut("drawWins/{drawNumber}")]
        public async Task DrawWinsAsync(int drawNumber)
        {
            Draw draw = _drawRepository.Get(drawNumber);
            draw = await _lottery.DrawWinsAsync(draw);
            _drawRepository.Update(draw, draw.Number);
        }
    }
}
