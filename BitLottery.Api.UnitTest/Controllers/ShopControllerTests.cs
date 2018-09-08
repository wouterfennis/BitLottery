using BitLottery.Api.Controllers;
using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Database.Interfaces;
using BitLottery.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitLottery.Api.UnitTests.Controllers
{
    [TestClass]
    public class ShopControllerTests
    {
        private Mock<ILottery> lotteryMock;
        private Mock<ICustomerRepository> customerRepositoryMock;
        private Mock<IDrawRepository> drawRepositoryMock;
        private Mock<IBallotRepository> ballotRepositoryMock;
        private ShopController shopController;

        [TestInitialize]
        public void Initialize()
        {
            lotteryMock = new Mock<ILottery>(MockBehavior.Strict);
            customerRepositoryMock = new Mock<ICustomerRepository>(MockBehavior.Strict);
            drawRepositoryMock = new Mock<IDrawRepository>(MockBehavior.Strict);
            ballotRepositoryMock = new Mock<IBallotRepository>(MockBehavior.Strict);

            shopController = new ShopController(lotteryMock.Object,
                customerRepositoryMock.Object,
                drawRepositoryMock.Object,
                ballotRepositoryMock.Object);
        }

        [TestMethod]
        public async Task SellBallotAsync_CallsBusinessAndRepositoryLayer_ReturnsCorrectBallotNumberAsync()
        {
            // Arrange
            int expectedNumberOfBallots = 10;
            int expectedBallotId = 111;
            int expectedDrawNumber = 222;
            int expectedCustomerNumber = 123456;
            DateTime expectedSellUntilDate = new DateTime(2018, 1, 1);

            var expectedBallot = new Ballot
            {
                Id = expectedBallotId,
                Number = 123456789
            };

            var expectedDraw = new Draw
            {
                Number = expectedDrawNumber,
                SellUntilDate = expectedSellUntilDate,
                Ballots = new List<Ballot>
                {
                    expectedBallot
                }
            };

            var expectedCustomer = new Customer
            {
                Number = expectedCustomerNumber,
                Name = "Cliff Bellman",
                Ballots = new List<Ballot>()
            };

            drawRepositoryMock
                .Setup(mock => mock.Get(expectedDrawNumber))
                .Returns(expectedDraw);

            customerRepositoryMock
                .Setup(mock => mock.Get(expectedCustomerNumber))
                .Returns(expectedCustomer);

            customerRepositoryMock
                .Setup(mock => mock.Update(expectedCustomer, expectedCustomerNumber))
                .Returns(true);

            ballotRepositoryMock
                .Setup(mock => mock.Update(expectedBallot, expectedBallotId))
                .Returns(true);

            lotteryMock
                .Setup(mock => mock.SellBallotAsync(expectedDraw))
                .ReturnsAsync(expectedBallot);

            var drawConfiguration = new DrawConfiguration
            {
                SellUntilDate = expectedSellUntilDate,
                NumberOfBallots = expectedNumberOfBallots
            };

            // Act
            int result = await shopController.SellBallotAsync(expectedCustomerNumber, expectedDrawNumber);

            // Assert
            result.Should().Be(expectedBallot.Number);

            expectedCustomer.Ballots.Count.Should().Be(1);
            var firstBallot = expectedCustomer.Ballots.Single();
            firstBallot.Should().Be(expectedBallot);

            lotteryMock.VerifyAll();
            drawRepositoryMock.VerifyAll();
            ballotRepositoryMock.VerifyAll();
            customerRepositoryMock.VerifyAll();
        }
    }
}
