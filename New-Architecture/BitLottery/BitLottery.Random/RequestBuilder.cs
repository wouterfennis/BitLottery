using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService
{

  public class RequestBuilder
  {
    private string _jsonRpc;
    internal RequestBuilder AddJsonRpc(string jsonRpc)
    {
      _jsonRpc = jsonRpc;
      return this;
    }
  }
}
