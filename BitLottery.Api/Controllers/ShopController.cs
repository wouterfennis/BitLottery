using BitLottery.Api.Controllers.Interfaces;
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

        [HttpPut("buyBallot/{drawNumber}")]
        public async Task<int> SellBallotAsync(int customerNumber, int drawNumber)
        {
            Draw draw = _drawRepository.Get(drawNumber);
            Customer customer = _customerRepository.Get(customerNumber);

            Ballot soldBallot = await _lottery.SellBallotAsync(draw);
            customer.AddBallot(soldBallot);
            _ballotRepository.Update(soldBallot, soldBallot.Id);
            _customerRepository.Update(customer, customer.Number);
            return soldBallot.Number;
        }
    }
}
