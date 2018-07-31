using BitLottery.RandomService.Models;

namespace BitLottery.RandomService
{
  /// <summary>
  /// Interface for implementing the builder pattern
  /// </summary>
  public interface IBuilder
  {
    /// <summary>
    /// Builds the final object.
    /// </summary>
    GenerateIntegersRequest Build();
  }
}
