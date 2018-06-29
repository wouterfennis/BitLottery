using BitLottery.RandomService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitLottery.RandomService.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1Async()
        {
            // Arrange
            Settings settings = new Settings
            {
                NumberOfIntegers = 1,
                MinimalIntValue = 1,
                MaximumIntValue = 4
            };

            //IRandomService randomService = new RandomDotOrgService(settings);

            // Act
           // var result = await randomService.GenerateRandomNumbersAsync();

            //result.ToString();
            // Assert
        }
    }
}
