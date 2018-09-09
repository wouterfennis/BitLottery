using BitLottery.Api.Controllers.Interfaces;
using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Database.Interfaces;
using BitLottery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BitLottery.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShopController : Controller, IShopController
    {
        private readonly ILottery _lottery;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly IBallotRepository _ballotRepository;

        public ShopController(ILottery lottery,
            ICustomerRepository customerRepository,
            IDrawRepository drawRepository,
            IBallotRepository ballotRepository)
        {
            _lottery = lottery;
            _customerRepository = customerRepository;
            _drawRepository = drawRepository;
            _ballotRepository = ballotRepository;
        }

        [HttpPut]
        public async Task<int> SellBallotAsync(SaleInfo saleInfo)
        {
            Draw draw = _drawRepository.Get(saleInfo.DrawNumber);
            Customer customer = _customerRepository.Get(saleInfo.CustomerNumber);

            Ballot soldBallot = await _lottery.SellBallotAsync(draw, saleInfo.LastNumber);

            customer.AddBallot(soldBallot);
            _ballotRepository.Update(soldBallot, soldBallot.Id);
            _customerRepository.Update(customer, customer.Number);

            return soldBallot.Id;
        }
    }
}
