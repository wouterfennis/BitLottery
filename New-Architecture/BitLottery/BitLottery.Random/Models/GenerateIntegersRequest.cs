using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService.Models
{
  public class GenerateIntegersRequest
  {
    /// <summary>
    /// The version identifier of the api
    /// </summary>
    public string jsonrpc { get; set; }

    /// <summary>
    /// The name of the method to be invoked
    /// </summary>
    public string method { get; set; }

    /// <summary>
    /// The parameters that will be supplied to the method.
    /// </summary>
    public GenerateIntegerParams @params { get; set; }

    /// <summary>
    /// The request identifier
    /// </summary>
    public int id { get; set; }
  }
}
