using BitLottery.Business;
using BitLottery.Business.Exceptions;
using BitLottery.Business.RandomGenerator;
using BitLottery.Models;
using BitLottery.Utilities.SystemTime;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.Business.UnitTests
{
    [TestClass]
    public class SellBallotAsyncTests : LotteryTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task SellBallotAsync_AfterDrawDate_ThrowsException()
        {
            // Arrange
            DateTime expectedSellDate = new DateTime(2018, 01, 02);

            SystemTime.SetDateTime(expectedSellDate);

            var expectedDraw = new Draw
            {
                DrawDate = new DateTime(2018, 1, 1),
                Ballots = new List<Ballot>
                    {
                    new Ballot {
                        Number = 12345,
                    },
                    new Ballot {
                        Number = 54321,
                    }
                }
            };

            // Act
            try
            {
                Ballot result = await Lottery.SellBallotAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                // Assert
                exception.Message.Should().Be($"The SellDate: {expectedSellDate} cannot be after the DrawDate: {expectedDraw.DrawDate}");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task SellBallotAsync_WhenAllBallotsAreSold_ThrowsException()
        {
            // Arrange
            DateTime expectedSellDate = new DateTime(2017, 12, 27);

            SystemTime.SetDateTime(expectedSellDate);

            var expectedBallots = new List<Ballot>
            {
                new Ballot {
                    Number = 12345,
                    SellDate = new DateTime(2017, 12,26)
                },
                new Ballot {
                    Number = 54321,
                    SellDate = new DateTime(2017, 12,25)
                }
            };

            var expectedDraw = new Draw
            {
                DrawDate = new DateTime(2018, 1, 1),
                Ballots = expectedBallots
            };

            // Act
            try
            {
                Ballot result = await Lottery.SellBallotAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                exception.Message.Should().Be("There are no more ballots for sale for this draw");
                throw;
            }
        }

        [TestMethod]
        public async Task SellBallotAsync_PicksRandomBallotRegistersAsSold()
        {
            // Arrange
            int expectedRandomNumber = 2;
            var expectedRandomNumbers = new List<int> { expectedRandomNumber };
            int expectedBallotNumber = 123456789;
            DateTime expectedSellDate = new DateTime(2017, 12, 25);

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
            RandomGeneratorMock
              .Setup(mock => mock.GenerateRandomNumbersAsync(It.IsAny<GenerationSettings>()))
              .Callback((GenerationSettings generationSettings) =>
              {
                  actualGenerationSettings = generationSettings;
              })
              .ReturnsAsync(expectedRandomNumbers);

            // Act
            Ballot result = await Lottery.SellBallotAsync(expectedDraw);

            // Assert
            result.Should().NotBeNull();
            result.Number.Should().Be(expectedBallotNumber);
            result.SellDate.Should().Be(expectedSellDate);

            actualGenerationSettings.Should().NotBeNull();
            actualGenerationSettings.NumberOfIntegers.Should().Be(1);
            actualGenerationSettings.MinimalIntValue.Should().Be(0);
            actualGenerationSettings.MaximumIntValue.Should().Be(2);

            RandomGeneratorMock.VerifyAll();
        }
    }
}
