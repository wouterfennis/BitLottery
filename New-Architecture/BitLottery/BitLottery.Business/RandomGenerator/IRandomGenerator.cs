using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.Business.RandomGenerator
{
    /// <summary>
    /// Logic for random calculations
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Generates a variable set of random numbers
        /// </summary>
        System.Threading.Tasks.Task<IEnumerable<int>> GenerateRandomNumbersAsync(GenerationSettings settings);
    }
}
