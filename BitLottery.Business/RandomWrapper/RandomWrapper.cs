using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.Business.RandomWrapper
{
  /// <inheritdoc/>
  public class RandomWrapper : IRandomWrapper
  {
    private Random Random { get; set; }

    /// <inheritdoc/>
    public int Next(int minValue, int maxValue)
    {
      return Random.Next(minValue, maxValue);
    }

    /// <inheritdoc/>
    public void Seed(int seed)
    {
      Random = new Random(seed);
    }
  }
}
