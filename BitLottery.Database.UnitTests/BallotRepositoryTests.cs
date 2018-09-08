using BitLottery.Database.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitLottery.Database.UnitTests
{
    [TestClass]
    public class BallotRepositoryTests : RepositoryTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Get_NoBallotFound_ThrowsException()
        {
            // Arrange
            int expectedBallotnumber = 123456;

            using (var context = new BitLotteryContext(Options))
            {
                BallotRepository ballotRepository = new BallotRepository(context);

                try
                {
                    // Act
                    ballotRepository.Get(expectedBallotnumber);
                }
                catch (Exception exception)
                {
                    // Assert
                    exception.Message.Should().Be("BallotId: 123456 not found");
                    throw;
                }
            }
        }
    }
}
