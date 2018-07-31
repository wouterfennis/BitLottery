using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
  /// <summary>
  /// The response returned by the RandomDotOrg API
  /// </summary>
  public class GenerateIntegersResponse
  {
    /// <summary>
    /// The version identifier of the api
    /// </summary>
    public string jsonrpc { get; set; }

    /// <summary>
    /// The result of the GenerateIntegers method
    /// </summary>
    public Result result { get; set; }

    /// <summary>
    /// The request identifier
    /// </summary>
    public int Id { get; set; }
  }
}
