using BitLottery.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BitLottery.Entities.UnitTests.Models
{
    [TestClass]
    public class DrawTests
    {
        [TestMethod]
        public void GetUnsoldBallots_ReturnsCorrectBallots()
        {
            // Arrange
            var ballots = new List<Ballot>
            {
                new Ballot
                {
                    Number = 4321
                },
                new Ballot
                {
                    Number = 9876
                },
                new Ballot
                {
                    Id = 1,
                    Number = 1234,
                    SellDate = new DateTime(2017, 12, 22)
                }
            };

            var draw = new Draw
            {
                Ballots = ballots
            };

            // Act
            IEnumerable<Ballot> result = draw.GetUnsoldBallots();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            var firstBallot = result.ElementAt(0);
            var secondBallot = result.ElementAt(1);

            firstBallot.Should().Be(ballots.ElementAt(0));
            secondBallot.Should().Be(ballots.ElementAt(1));
        }
    }
}
