using BitLottery.Business.RandomGenerator;
using BitLottery.Entities.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Business.UnitTests
{
    [TestClass]
    public class GenerateDrawAsyncTests : LotteryTestBase
    {
        [TestMethod]
        public async Task GenerateDrawAsync_ReturnsCorrectDrawAsync()
        {
            // Arrange
            int expectedSeed = 1;
            int expectedNumberOfBallots = 1;
            var expectedRandomNumbers = new List<int> { expectedSeed };
            var expectedBallotNumber = 12345;

            GenerationSettings actualGenerationSettings = null;
            RandomGeneratorMock
              .Setup(mock => mock.GenerateRandomNumbersAsync(It.IsAny<GenerationSettings>()))
              .Callback((GenerationSettings generationSettings) =>
              {
                  actualGenerationSettings = generationSettings;
              })
              .ReturnsAsync(expectedRandomNumbers);

            RandomWrapperMock
              .Setup(mock => mock.Seed(expectedSeed));

            RandomWrapperMock
              .Setup(mock => mock.Next(1000000, 10000000))
              .Returns(expectedBallotNumber);

            // Act
            IEnumerable<Ballot> result = await Lottery.GenerateBallotsAsync(expectedNumberOfBallots);

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedNumberOfBallots);

            Ballot firstBallot = result.First();
            firstBallot.Number.Should().Be(expectedBallotNumber);
            firstBallot.SellDate.Should().BeNull();

            actualGenerationSettings.Should().NotBeNull();
            actualGenerationSettings.NumberOfIntegers.Should().Be(1);
            actualGenerationSettings.MinimalIntValue.Should().Be(1000000);
            actualGenerationSettings.MaximumIntValue.Should().Be(10000000);

            RandomGeneratorMock.VerifyAll();
            RandomWrapperMock.VerifyAll();
        }
    }
}
