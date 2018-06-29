using BitLottery.RandomService.HttpClientWrapper;
using BitLottery.RandomService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

            var c = Directory.GetCurrentDirectory();

            // Read configuration
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
            var request = new GenerateIntegersRequest()
            {
                jsonrpc = "2.0",
                method = "generateIntegers",
                @params = new GenerateIntegerParams
                {
                    apiKey = _apiKey,
                    n = settings.NumberOfIntegers,
                    min = settings.MinimalIntValue,
                    max = settings.MaximumIntValue,
                    replacement = true,
                    @base = 10

                },
                id = 1
            };

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(_apiUrl, httpContent);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<GenerateIntegersResponse>(responseString);

            return response.result.random.data;
        }
    }
}
