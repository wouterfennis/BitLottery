using BitLottery.Business;
using BitLottery.Business.RandomGenerator;
using BitLottery.Business.RandomWrapper;
using BitLottery.Business.SystemTime;
using BitLottery.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitlottery.Business.UnitTests
{
    [TestClass]
    public class LotteryTests
    {
        private Mock<IRandomGenerator> _randomGeneratorMock { get; set; }
        private Mock<IRandomWrapper> _randomWrapperMock { get; set; }
        private Lottery _lottery { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            _randomGeneratorMock = new Mock<IRandomGenerator>(MockBehavior.Strict);
            _randomWrapperMock = new Mock<IRandomWrapper>(MockBehavior.Strict);

            _lottery = new Lottery(_randomGeneratorMock.Object, _randomWrapperMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public async Task GenerateDrawAsync_ReturnsCorrectDrawAsync()
        {
            // Arrange
            int expectedSeed = 1;
            DateTime expectedDrawDate = new DateTime(2018, 1, 1);
            int expectedNumberOfBallots = 1;
            var expectedRandomNumbers = new List<int> { expectedSeed };
            var expectedBallotNumber = 12345;

            GenerationSettings actualGenerationSettings = null;
            _randomGeneratorMock
              .Setup(mock => mock.GenerateRandomNumbersAsync(It.IsAny<GenerationSettings>()))
              .Callback((GenerationSettings generationSettings) =>
              {
                  actualGenerationSettings = generationSettings;
              })
              .ReturnsAsync(expectedRandomNumbers);

            _randomWrapperMock
              .Setup(mock => mock.Seed(expectedSeed));

            _randomWrapperMock
              .Setup(mock => mock.Next(1000000, 10000000))
              .Returns(expectedBallotNumber);

            // Act
            Draw result = await _lottery.GenerateDrawAsync(expectedDrawDate, expectedNumberOfBallots);

            // Assert
            result.Should().NotBeNull();
            result.Ballots.Should().NotBeNull();
            result.Ballots.Should().HaveCount(expectedNumberOfBallots);

            Ballot firstBallot = result.Ballots.First();
            firstBallot.Number.Should().Be(expectedBallotNumber);
            firstBallot.SellDate.Should().BeNull();

            result.DrawDate.Should().Be(expectedDrawDate);

            actualGenerationSettings.Should().NotBeNull();
            actualGenerationSettings.NumberOfIntegers.Should().Be(1);
            actualGenerationSettings.MinimalIntValue.Should().Be(1000000);
            actualGenerationSettings.MaximumIntValue.Should().Be(10000000);

            _randomGeneratorMock.VerifyAll();
            _randomWrapperMock.VerifyAll();
        }

        [TestMethod]
        public async Task SellBallotAsync_PicksRandomBallotRegistersAsSold()
        {
            // Arrange
            int expectedRandomNumber = 2;
            var expectedRandomNumbers = new List<int> { expectedRandomNumber };
            int expectedBallotNumber = 123456789;
            DateTime expectedSellDate = new DateTime(2017,12,25);

            SystemTime.SetDateTime(expectedSellDate);

            var expectedBallots = new List<Ballot>
            {
                new Ballot(),
                new Ballot(),
                new Ballot()
                {
                    Number = expectedBallotNumber
                }
            };

            var expectedDraw = new Draw
            {
                DrawDate = new DateTime(2018, 1, 1),
                Ballots = expectedBallots
            };

            GenerationSettings actualGenerationSettings = null;
            _randomGeneratorMock
              .Setup(mock => mock.GenerateRandomNumbersAsync(It.IsAny<GenerationSettings>()))
              .Callback((GenerationSettings generationSettings) =>
              {
                  actualGenerationSettings = generationSettings;
              })
              .ReturnsAsync(expectedRandomNumbers);

            // Act
            Ballot result = await _lottery.SellBallotAsync(expectedDraw);

            // Assert
            result.Should().NotBeNull();
            result.Number.Should().Be(expectedBallotNumber);
            result.SellDate.Should().Be(expectedSellDate);

            actualGenerationSettings.Should().NotBeNull();
            actualGenerationSettings.NumberOfIntegers.Should().Be(1);
            actualGenerationSettings.MinimalIntValue.Should().Be(0);
            actualGenerationSettings.MaximumIntValue.Should().Be(2);

            _randomGeneratorMock.VerifyAll();
        }
    }
}
