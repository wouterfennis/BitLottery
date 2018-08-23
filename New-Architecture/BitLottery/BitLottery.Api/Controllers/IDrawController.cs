using System.Threading.Tasks;
using BitLottery.Models;

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
    Task<int> GenerateDraw();

    /// <summary>
    /// Retrieves a specific draw
    /// </summary>
    /// <param name="id">The id of the draw</param>
    Draw GetDraw(int id);
  }
}
