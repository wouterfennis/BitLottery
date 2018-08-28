using BitLottery.RandomService.HttpClientWrapper;
using BitLottery.RandomService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace BitLottery.RandomService
{
  public class RandomDotOrgService : IRandomService
  {
    private readonly IHttpClientWrapper _httpClient;
    private string _apiUrl;
    private readonly string _apiKey;

    public RandomDotOrgService(IHttpClientWrapper httpClientWrapper)
    {
      _httpClient = httpClientWrapper;

      var currentDirectory = Directory.GetCurrentDirectory();

      var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true, true)
                  .Build();

      var appsettings = config.GetSection("appsettings");
      _apiUrl = appsettings["APIUrl"];
      _apiKey = appsettings["randomDotOrgAPIKey"];
    }

    public async System.Threading.Tasks.Task<IEnumerable<int>> GenerateRandomNumbersAsync(Settings settings)
    {
      var requestBuilder = new GenerateIntegersRequestBuilder();
      GenerateIntegersRequest request = requestBuilder
        .AddJsonRpc("2.0")
        .AddMethod("generateIntegers")
        .AddApiKey(_apiKey)
        .AddNumberOfIntegers(settings.NumberOfIntegers)
        .AddMinimalValue(settings.MinimalIntValue)
        .AddMaximumValue(settings.MaximumIntValue)
        .AddReplacement(true)
        .AddBase(10)
        .AddId(1)
        .Build();
        
      string json = JsonConvert.SerializeObject(request);
      var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

      HttpResponseMessage httpResponse = await _httpClient.PostAsync(_apiUrl, httpContent);
      string responseString = await httpResponse.Content.ReadAsStringAsync();

      GenerateIntegersResponse response = JsonConvert.DeserializeObject<GenerateIntegersResponse>(responseString);

      return response.result.random.data;
    }
  }
}
