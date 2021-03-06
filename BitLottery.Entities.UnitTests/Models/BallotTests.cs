﻿using BitLottery.Entities.Models;
using BitLottery.Utilities.SystemTime;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitLottery.Entities.UnitTests.Models
{
    [TestClass]
    public class BallotTest
    {
        [TestCleanup]
        public void CleanUp()
        {
            SystemTime.ResetDateTime();
        }

        [TestMethod]
        public void RegisterAsSold_AddsSellDateToBallot()
        {
            // Arrange
            var expectedDateTime = new DateTime(2018, 01, 01);
            SystemTime.SetDateTime(expectedDateTime);

            var ballot = new Ballot
            {
                Id = 1,
                Number = 1234,
            };

            // Act
            ballot.RegisterAsSold();

            // Assert
            ballot.Id.Should().Be(ballot.Id);
            ballot.Number.Should().Be(ballot.Number);
            ballot.SellDate.Should().Be(expectedDateTime);
        }

        [TestMethod]
        public void GetLastDigit_ReturnsLastDigitOfNumber()
        {
            // Arrange
            var ballot = new Ballot
            {
                Number = 1234,
            };

            // Act
            var result = ballot.GetLastDigit();

            // Assert
            result.Should().Be(4);
        }
    }
}
