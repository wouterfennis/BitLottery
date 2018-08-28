using BitLottery.Business;
using BitLottery.Models;
using BitLottery.Business.RandomGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using BitLottery.Business.RandomWrapper;
using System.Linq;
using System;

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

    [TestMethod]
    public async Task GenerateDrawAsync_ReturnsDrawAsync()
    {
      // Arrange
      int expectedSeed = 1;
      DateTime expectedDrawDate = new DateTime(2018, 1, 1);
      int expectedNumberOfBallots = 222;
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
        .Setup(mock => mock.Next(0, 10000000))
        .Returns(expectedBallotNumber);

      // Act
      Draw draw = await _lottery.GenerateDrawAsync(expectedDrawDate, expectedNumberOfBallots);

      // Assert
      draw.Should().NotBeNull();
      draw.Ballots.Should().NotBeNull();
      draw.Ballots.Should().HaveCount(222);
      draw.Ballots.Should();

      Ballot firstBallot = draw.Ballots.First();
      firstBallot.Number.Should().Be(expectedBallotNumber);
      firstBallot.SellDate.Should().BeNull();

      draw.DrawDate.Should().Be(expectedDrawDate);

      actualGenerationSettings.Should().NotBeNull();
      actualGenerationSettings.NumberOfIntegers.Should().Be(1);
      actualGenerationSettings.MinimalIntValue.Should().Be(1000000);
      actualGenerationSettings.MaximumIntValue.Should().Be(10000000);

      _randomGeneratorMock.VerifyAll();
      _randomWrapperMock.VerifyAll();
    }
  }
}
