using BitLottery.Api.Controllers;
using BitLottery.Api.Models;
using BitLottery.Business;
using BitLottery.Database;
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
        private Mock<IRepository<Draw, int>> drawRepositoryMock;
        private Mock<IRepository<Ballot, int>> ballotRepositoryMock;
        private DrawController drawController;

        [TestInitialize]
        public void Initalize()
        {
            lotteryMock = new Mock<ILottery>(MockBehavior.Strict);
            drawRepositoryMock = new Mock<IRepository<Draw, int>>(MockBehavior.Strict);
            ballotRepositoryMock = new Mock<IRepository<Ballot, int>>(MockBehavior.Strict);

            drawController = new DrawController(lotteryMock.Object, drawRepositoryMock.Object, ballotRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GenerateDraw_CallsBusinessLayer_CallsRepositoryLayerAsync()
        {
            // Arrange
            DateTime expectedDrawDate = new DateTime(2018, 1, 1);
            int expectedNumberOfBallots = 10;
            int expectedId = 222;
            var expectedDraw = new Draw
            {
                Id = expectedId,
                DrawDate = expectedDrawDate,
                Ballots = new List<Ballot>()
            };

            lotteryMock
              .Setup(mock => mock.GenerateDrawAsync(expectedDrawDate, expectedNumberOfBallots))
              .ReturnsAsync(expectedDraw);

            drawRepositoryMock.Setup(mock => mock.Insert(expectedDraw))
              .Returns(expectedId);

            var drawConfiguration = new DrawConfiguration
            {
                DrawDate = expectedDrawDate,
                NumberOfBallots = expectedNumberOfBallots
            };

            // Act
            int result = await drawController.GenerateDraw(drawConfiguration);

            // Assert
            result.Should().Be(expectedId);

            lotteryMock.VerifyAll();
            drawRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetDraw_CallsRepositoryLayer()
        {
            // Arrange
            int expectedId = 222;
            var expectedDraw = new Draw
            {
                Id = expectedId,
                DrawDate = new DateTime(2018, 1, 1)
            };

            drawRepositoryMock.Setup(mock => mock.Get(expectedId))
              .Returns(expectedDraw);

            // Act
            var result = drawController.GetDraw(expectedId);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedDraw);

            drawRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public async Task SellBallot_CallsBusinessLayer_CallsRepositoryLayerAsync()
        {
            // Arrange
            int expectedNumberOfBallots = 10;
            int expectedBallotId = 111;
            int expectedDrawId = 222;
            DateTime expectedDrawDate = new DateTime(2018, 1, 1);

            var expectedBallot = new Ballot
            {
                Id = expectedBallotId,
                Number = 123456789
            };

            var expectedDraw = new Draw
            {
                Id = expectedDrawId,
                DrawDate = expectedDrawDate,
                Ballots = new List<Ballot>()
            };

            lotteryMock
                .Setup(mock => mock.SellBallotAsync(expectedDraw))
                .ReturnsAsync(expectedBallot);

            drawRepositoryMock.Setup(mock => mock.Get(expectedDrawId))
                .Returns(expectedDraw);

            ballotRepositoryMock.Setup(mock => mock.Update(expectedBallot, expectedBallotId))
                .Returns(true);

            var drawConfiguration = new DrawConfiguration
            {
                DrawDate = expectedDrawDate,
                NumberOfBallots = expectedNumberOfBallots
            };

            // Act
            int result = await drawController.SellBallotAsync(expectedDrawId);

            // Assert
            result.Should().Be(expectedBallotId);

            lotteryMock.VerifyAll();
            drawRepositoryMock.VerifyAll();
            ballotRepositoryMock.VerifyAll();
        }
    }
}
