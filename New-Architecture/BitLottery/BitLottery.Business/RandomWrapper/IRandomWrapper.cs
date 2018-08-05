namespace BitLottery.Business.RandomWrapper
{
  /// <summary>
  /// Wrapper for the System.Random
  /// </summary>
  public interface IRandomWrapper
  {
    /// <summary>
    /// Initializes a new instance of the System.Random class, using the specified seed value.
    /// </summary>
    /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
    void Seed(int seed);

    /// <summary>
    /// Returns a random integer that is within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
    int Next(int minValue, int maxValue);
  }
}
