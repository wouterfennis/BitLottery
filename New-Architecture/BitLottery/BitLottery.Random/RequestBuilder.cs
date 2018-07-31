using BitLottery.RandomService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitLottery.RandomService
{
  /// </inheritdoc>
  public class GenerateIntegersRequestBuilder : IBuilder
  {
    private string _jsonRpc;
    private string _method;
    private string _apiKey;
    private int _numberOfIntegers;
    private int _minimalIntValue;
    private int _maximumIntValue;
    private bool _replacement;
    private int _base;
    private int _id;

    public GenerateIntegersRequestBuilder AddJsonRpc(string jsonRpc)
    {
      _jsonRpc = jsonRpc;
      return this;
    }

    public GenerateIntegersRequestBuilder AddMethod(string method)
    {
      _method = method;
      return this;
    }

    public GenerateIntegersRequestBuilder AddApiKey(string apiKey)
    {
      _apiKey = apiKey;
      return this;
    }

    public GenerateIntegersRequestBuilder AddNumberOfIntegers(int numberOfIntegers)
    {
      _numberOfIntegers = numberOfIntegers;
      return this;
    }

    public GenerateIntegersRequestBuilder AddMinimalValue(int minimalIntValue)
    {
      _minimalIntValue = minimalIntValue;
      return this;
    }

    public GenerateIntegersRequestBuilder AddMaximumValue(int maximumIntValue)
    {
      _maximumIntValue = maximumIntValue;
      return this;
    }

    public GenerateIntegersRequestBuilder AddReplacement(bool replacement)
    {
      _replacement = replacement;
      return this;
    }

    public GenerateIntegersRequestBuilder AddBase(int @base)
    {
      _base = @base;
      return this;
    }

    public GenerateIntegersRequestBuilder AddId(int id)
    {
      _id = id;
      return this;
    }

    /// </inheritdoc>
    public GenerateIntegersRequest Build()
    {
      var request = new GenerateIntegersRequest()
      {
        jsonrpc = _jsonRpc,
        method = _method,
        @params = new GenerateIntegerParams
        {
          apiKey = _apiKey,
          n = _numberOfIntegers,
          min = _minimalIntValue,
          max = _maximumIntValue,
          replacement = _replacement,
          @base = _base
        },
        id = _id
      };

      return request;
    }
  }
}
