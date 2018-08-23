using BitLottery.Api.Controllers;
using BitLottery.Business;
using BitLottery.Database;
using BitLottery.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
      int expectedId = 222;
      var expectedDraw = new Draw
      {
        Id = expectedId,
        DrawDate = new System.DateTime(2018, 1, 1)
      };

      lotteryMock
        .Setup(mock => mock.GenerateDrawAsync())
        .ReturnsAsync(expectedDraw);

      repositoryMock.Setup(mock => mock.Insert(expectedDraw))
        .Returns(expectedId);

      // Act
      int result = await drawController.GenerateDraw();

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
