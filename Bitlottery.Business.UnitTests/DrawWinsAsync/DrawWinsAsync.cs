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
    public class DrawWinsAsyncTests : LotteryTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task DrawWinsAsync_WhenDrawHasBeenDrawn_ThrowsException()
        {
            // Arrange
            var expectedDraw = new Draw
            {
                SellUntilDate = new DateTime(2017, 12, 31, 23, 59, 59),
                DrawDate = new DateTime(2018, 01, 01),
                Ballots = new List<Ballot>
                    {
                    new Ballot {
                        Number = 12345,
                        SellDate = new DateTime(2017, 12, 30)
                    },
                    new Ballot {
                        Number = 54321,
                        SellDate = new DateTime(2017, 12, 30)
                    }
                }
            };

            // Act
            try
            {
                Ballot result = await Lottery.DrawWinsAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                // Assert
                exception.Message.Should().Be($"This draw has already been drawn at { expectedDraw.DrawDate }");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task DrawWinsAsync_BeforeSellUntilDate_ThrowsException()
        {
            // Arrange
            DateTime expectedDrawDate = new DateTime(2017, 12, 30);

            SystemTime.SetDateTime(expectedDrawDate);

            var expectedDraw = new Draw
            {
                SellUntilDate = new DateTime(2018, 1, 1),
                Ballots = new List<Ballot>
                    {
                    new Ballot {
                        Number = 12345,
                        SellDate = new DateTime(2017, 12, 30)
                    },
                    new Ballot {
                        Number = 54321,
                        SellDate = new DateTime(2017, 12, 30)
                    }
                }
            };

            // Act
            try
            {
                Ballot result = await Lottery.DrawWinsAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                // Assert
                exception.Message.Should().Be($"The DrawDate: {expectedDrawDate} cannot be before the SellUntilDate: {expectedDraw.SellUntilDate}");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task DrawWinsAsync_WhenNoBallotsAreSold_ThrowsException()
        {
            // Arrange
            DateTime expectedDrawDate = new DateTime(2018, 1, 1);

            SystemTime.SetDateTime(expectedDrawDate);

            var expectedBallots = new List<Ballot>
            {
                new Ballot {
                    Number = 12345,
                },
                new Ballot {
                    Number = 54321,
                }
            };

            var expectedDraw = new Draw
            {
                SellUntilDate = new DateTime(2018, 1, 1),
                Ballots = expectedBallots
            };

            // Act
            try
            {
                Ballot result = await Lottery.DrawWinsAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                exception.Message.Should().Be("There are not enough ballots sold for this draw");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DrawException))]
        public async Task DrawWinsAsync_WhenOneBallotIsSold_ThrowsException()
        {
            // Arrange
            DateTime expectedDrawDate = new DateTime(2018, 1, 1);

            SystemTime.SetDateTime(expectedDrawDate);

            var expectedBallots = new List<Ballot>
            {
                new Ballot {
                    Number = 12345,
                },
                new Ballot {
                    Number = 54321,
                    SellDate = new DateTime(2017,12,31)
                }
            };

            var expectedDraw = new Draw
            {
                SellUntilDate = new DateTime(2018, 1, 1),
                Ballots = expectedBallots
            };

            // Act
            try
            {
                Ballot result = await Lottery.DrawWinsAsync(expectedDraw);
            }
            catch (Exception exception)
            {
                exception.Message.Should().Be("There are not enough ballots sold for this draw");
                throw;
            }
        }

        [TestMethod]
        public async Task DrawWinsAsync_PicksRandomBallotRegistersAsWinner()
        {
            // Arrange
            int expectedRandomNumber = 1;
            var expectedRandomNumbers = new List<int> { expectedRandomNumber };
            DateTime expectedDrawDate = new DateTime(2018, 01, 01);

            SystemTime.SetDateTime(expectedDrawDate);

            var winningBallot = new Ballot
            {
                Number = 123456789,
                SellDate = new DateTime(2017, 12, 30)
            };

            var expectedBallots = new List<Ballot>
            {
                new Ballot {
                    Number = 12345,
                    SellDate = new DateTime(2017, 12, 30)
                },
                winningBallot,
                new Ballot {
                    Number = 54321,
                    SellDate = new DateTime(2017, 12, 30)
                },
            };

            var expectedDraw = new Draw
            {
                SellUntilDate = new DateTime(2018, 1, 1),
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
            Ballot result = await Lottery.DrawWinsAsync(expectedDraw);

            // Assert
            result.Should().NotBeNull();
            result.Number.Should().Be(winningBallot.Number);
            result.SellDate.Should().Be(winningBallot.SellDate);
            result.IsWinner.Should().BeTrue();

            expectedDraw.DrawDate.Should().Be(expectedDrawDate);

            actualGenerationSettings.Should().NotBeNull();
            actualGenerationSettings.NumberOfIntegers.Should().Be(1);
            actualGenerationSettings.MinimalIntValue.Should().Be(0);
            actualGenerationSettings.MaximumIntValue.Should().Be(2);

            RandomGeneratorMock.VerifyAll();
        }
    }
}
