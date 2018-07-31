using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
  /// <summary>
  /// The result returned by the RandomDotOrg API
  /// </summary>
  public class Result
  {
    /// <summary>
    /// The RandomResponse containing the random data
    /// </summary>
    public RandomResponse random { get; set; }

    /// <summary>
    /// An integer containing the number of true random bits used to complete this request.
    /// </summary>
    public int bitsUsed { get; set; }

    /// <summary>
    /// An integer containing the (estimated) number of remaining true random bits available to the client.
    /// </summary>
    public int bitsLeft { get; set; }

    /// <summary>
    /// An integer containing the (estimated) number of remaining API requests available to the client.
    /// </summary>
    public int requestsLeft { get; set; }

    /// <summary>
    /// An integer containing the recommended number of milliseconds that the client should delay before issuing another request.
    /// </summary>
    public int advisoryDelay { get; set; }
  }
}
