using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.Business.RandomWrapper
{
  /// <inheritdoc/>
  public class RandomWrapper : IRandomWrapper
  {
    private Random _random { get; set; }

    /// <inheritdoc/>
    public int Next(int minValue, int maxValue)
    {
      return _random.Next(minValue, maxValue);
    }

    /// <inheritdoc/>
    public void Seed(int seed)
    {
      _random = new Random(seed);
    }
  }
}
