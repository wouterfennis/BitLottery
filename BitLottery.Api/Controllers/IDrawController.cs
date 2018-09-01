using BitLottery.Api.Models;
using BitLottery.Models;
using System.Threading.Tasks;

namespace BitLottery.Controllers
{
    /// <summary>
    /// Exposes methods to retrieve and create lottery draws
    /// </summary>
    public interface IDrawController
    {
        /// <summary>
        /// Creates a new draw
        /// </summary>
        /// <param name="id">The configuration of the draw</param>
        /// <returns>The id of the new draw</returns>
        Task<int> GenerateDraw(DrawConfiguration drawConfiguration);

        /// <summary>
        /// Retrieves a specific draw
        /// </summary>
        /// <param name="id">The id of the draw</param>
        /// <returns>The wanted draw</returns>
        Draw GetDraw(int id);

        /// <summary>
        /// Registers a ballot as sold
        /// </summary>
        /// <param name="drawId">The id of the draw</param>
        /// <returns>Id of the sold ballot</returns>
        Task<int> SellBallotAsync(int drawId);
    }
}
