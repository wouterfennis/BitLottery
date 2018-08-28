namespace BitLottery.RandomService.Models
{
  /// <summary>
  /// The parameters for the GenerateIntegers method
  /// </summary>
  public class GenerateIntegerParams
  {
    /// <summary>
    /// The api key
    /// </summary>
    public string apiKey { get; set; }

    /// <summary>
    /// The number of integers requested
    /// </summary>
    public int n { get; set; }

    /// <summary>
    /// The minimal value of the integer
    /// </summary>
    public int min { get; set; }

    /// <summary>
    /// The maximum value of the integer
    /// </summary>
    public int max { get; set; }

    /// <summary>
    /// Specifies whether the random numbers should be picked with replacement
    /// </summary>
    public bool replacement { get; set; }

    /// <summary>
    /// Specifies the base that will be used to display the numbers
    /// </summary>
    public int @base { get; set; }
  }
}
