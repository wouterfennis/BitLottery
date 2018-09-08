using BitLottery.Database.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitLottery.Database.UnitTests
{
    [TestClass]
    public class DrawRepositoryTests : RepositoryTestsBase
    {

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Get_NoDrawFound_ThrowsException()
        {
            // Arrange
            int expectedDrawnumber = 123456;

            using (var context = new BitLotteryContext(Options))
            {
                DrawRepository drawRepository = new DrawRepository(context);

                try
                {
                    // Act
                    drawRepository.Get(expectedDrawnumber);
                }
                catch (Exception exception)
                {
                    // Assert
                    exception.Message.Should().Be("Drawnumber: 123456 not found");
                    throw;
                }
            }
        }
    }
}
