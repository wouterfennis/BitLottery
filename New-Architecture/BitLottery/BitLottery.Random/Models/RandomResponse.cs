using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
  /// <summary>
  /// This object encapsulates the random numbers and associated data
  /// </summary>
  public class RandomResponse
    {
    /// <summary>
    /// An array containing the sequence of numbers requested.
    /// </summary>
    public IEnumerable<int> data { get; set; }

    /// <summary>
    /// A string containing the timestamp in ISO 8601 format at which the request was completed.
    /// </summary>
    public DateTime completionTime { get; set; }
    }
}
