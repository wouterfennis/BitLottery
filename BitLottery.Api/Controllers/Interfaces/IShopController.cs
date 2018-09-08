using BitLottery.Api.Models;
using System.Threading.Tasks;

namespace BitLottery.Api.Controllers.Interfaces
{
    public interface IShopController
    {
        /// <summary>
        /// Registers a ballot as sold
        /// </summary>
        /// <param name="drawNumber">The number of the draw</param>
        /// <returns>number of the sold ballot</returns>
        Task<int> SellBallotAsync(int customberNumber, int drawNumber);
    }
}