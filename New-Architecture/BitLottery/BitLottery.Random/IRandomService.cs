using BitLottery.RandomService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.RandomService
{
    /// <summary>
    /// Interface for external Random Service
    /// </summary>
    public interface IRandomService
    {
        /// <summary>
        /// Generates a variable set of random numbers
        /// </summary>
        Task<IEnumerable<int>> GenerateRandomNumbersAsync(Settings settings);
    }
}