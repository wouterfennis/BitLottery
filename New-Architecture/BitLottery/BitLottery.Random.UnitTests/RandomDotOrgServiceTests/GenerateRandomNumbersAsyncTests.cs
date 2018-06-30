using BitLottery.RandomService.HttpClientWrapper;
using BitLottery.RandomService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitLottery.RandomService.UnitTests.RandomDotOrgServiceTests
{
  [TestClass]
  public class GenerateRandomNumbersAsyncTests
  {
    [TestMethod]
    public async Task GenerateRandomNumbersAsync_CreatesCorrectRequestAsync()
    {
      // Arrange
      string expectedJsonResponse = "{\"jsonrpc\":\"2.0\",\"result\":{\"random\":{\"data\":[2,5,1,1,5,7,9,7,10,10],\"completionTime\":\"2018 - 06 - 30 08:20:20Z\"},\"bitsUsed\":33,\"bitsLeft\":1876795,\"requestsLeft\":394736,\"advisoryDelay\":1070},\"id\":27964}";
      var expectedHttpResponseMessage = new HttpResponseMessage
      {
        Content = new StringContent(expectedJsonResponse)
      };

      var postResponse = Task.Run(() => expectedHttpResponseMessage);

      var httpClientWrapperMock = new Mock<IHttpClientWrapper>(MockBehavior.Strict);
      httpClientWrapperMock
        .Setup(mock => mock.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
        .Returns(postResponse);

      Settings settings = new Settings
      {
        NumberOfIntegers = 1,
        MinimalIntValue = 1,
        MaximumIntValue = 4
      };

      IRandomService randomService = new RandomDotOrgService(httpClientWrapperMock.Object);

      // Act
      IEnumerable<int> result = await randomService.GenerateRandomNumbersAsync(settings);

      // Assert
      result.Shou

    }
  }
}
