using BitLottery.Api.Models;
using System.Threading.Tasks;

namespace BitLottery.Api.Controllers.Interfaces
{
    public interface IShopController
    {
        /// <summary>
        ///  Registers a ballot as sold
        /// </summary>
        /// <param name="saleInfo">Information to make the sale</param>
        /// <returns>The id of the sold ballot</returns>
        Task<int> SellBallotAsync(SaleInfo saleInfo);
    }
}