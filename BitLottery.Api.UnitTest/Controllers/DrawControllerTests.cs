using BitLottery.Api.Controllers;
using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Database;
using BitLottery.Database.Interfaces;
using BitLottery.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitLottery.Api.UnitTests
{
    [TestClass]
    public class DrawControllerTests
    {
        private Mock<ILottery> lotteryMock;
        private Mock<IDrawRepository> drawRepositoryMock;
        private Mock<IBallotRepository> ballotRepositoryMock;
        private DrawController drawController;

        [TestInitialize]
        public void Initalize()
        {
            lotteryMock = new Mock<ILottery>(MockBehavior.Strict);
            drawRepositoryMock = new Mock<IDrawRepository>(MockBehavior.Strict);
            ballotRepositoryMock = new Mock<IBallotRepository>(MockBehavior.Strict);

            drawController = new DrawController(lotteryMock.Object, drawRepositoryMock.Object, ballotRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GenerateDraw_CallsBusinessAndRepositoryLayer_ReturnsCorrectDrawNumberAsync()
        {
            // Arrange
            DateTime expectedSellUntilDate = new DateTime(2018, 1, 1);
            int expectedNumberOfBallots = 10;
            int expectedNumber = 222;
            var expectedDraw = new Draw
            {
                Number = expectedNumber,
                SellUntilDate = expectedSellUntilDate,
                Ballots = new List<Ballot>()
            };

            lotteryMock
              .Setup(mock => mock.GenerateDrawAsync(expectedSellUntilDate, expectedNumberOfBallots))
              .ReturnsAsync(expectedDraw);

            drawRepositoryMock.Setup(mock => mock.Insert(expectedDraw))
              .Returns(expectedNumber);

            var drawConfiguration = new DrawConfiguration
            {
                SellUntilDate = expectedSellUntilDate,
                NumberOfBallots = expectedNumberOfBallots
            };

            // Act
            int result = await drawController.GenerateDraw(drawConfiguration);

            // Assert
            result.Should().Be(expectedNumber);

            lotteryMock.VerifyAll();
            drawRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetDraw_CallsRepositoryLayer_ReturnsCorrectDraw()
        {
            // Arrange
            int expectedNumber = 222;
            var expectedDraw = new Draw
            {
                Number = expectedNumber,
                Ballots = new Ballot[5],
                SellUntilDate = new DateTime(2018, 1, 1)
            };

            drawRepositoryMock.Setup(mock => mock.Get(expectedNumber))
              .Returns(expectedDraw);

            // Act
            var result = drawController.GetDraw(expectedNumber);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedDraw);
            
            drawRepositoryMock.VerifyAll();
        }
    }
}
