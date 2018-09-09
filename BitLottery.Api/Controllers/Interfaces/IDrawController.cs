using BitLottery.Api.Models;
using BitLottery.Entities.Models;
using System.Threading.Tasks;

namespace BitLottery.Controllers.Interfaces
{
    /// <summary>
    /// Exposes methods to retrieve and create lottery draws
    /// </summary>
    public interface IDrawController
    {
        /// <summary>
        /// Creates a new draw
        /// </summary>
        /// <param name="drawConfiguration">The configuration of the draw</param>
        /// <returns>The number of the new draw</returns>
        Task<int> GenerateDraw(DrawConfiguration drawConfiguration);

        /// <summary>
        /// Retrieves a specific draw
        /// </summary>
        /// <param name="drawNumber">The number of the draw</param>
        /// <returns>The wanted draw</returns>
        Draw GetDraw(int drawNumber);

        /// <summary>
        /// Draws the winning numbers for a draw
        /// </summary>
        /// <param name="drawNumber">The number of the draw</param>
        Task DrawWinsAsync(int drawNumber);
    }
}
