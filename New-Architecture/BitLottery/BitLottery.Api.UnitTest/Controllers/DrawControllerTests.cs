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

namespace BitLottery.Api.UnitTests
{
  [TestClass]
  public class DrawControllerTests
  {
    private Mock<ILottery> lotteryMock;
    private Mock<IRepository<Draw, int>> repositoryMock;
    private DrawController drawController;

    [TestInitialize]
    public void Initalize()
    {
      lotteryMock = new Mock<ILottery>(MockBehavior.Strict);
      repositoryMock = new Mock<IRepository<Draw, int>>(MockBehavior.Strict);

      drawController = new DrawController(lotteryMock.Object, repositoryMock.Object);
    }

    [TestMethod]
    public async System.Threading.Tasks.Task GenerateDraw_CallsBusinessLayer_CallsRepositoryLayerAsync()
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

      repositoryMock.Setup(mock => mock.Insert(expectedDraw))
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
      repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void GetDraw_CallsRepositoryLayer()
    {
      // Arrange
      int expectedId = 222;
      var expectedDraw = new Draw
      {
        Id = expectedId,
        DrawDate = new System.DateTime(2018, 1, 1)
      };

      repositoryMock.Setup(mock => mock.Get(expectedId))
        .Returns(expectedDraw);

      // Act
      var result = drawController.GetDraw(expectedId);

      // Assert
      result.Should().NotBeNull();
      result.Should().Be(expectedDraw);

      repositoryMock.VerifyAll();
    }
  }
}
