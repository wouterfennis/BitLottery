using BitLottery.Database.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BitLottery.Database.UnitTests
{
    [TestClass]
    public class CustomerRepositoryTests : RepositoryTestsBase
    {

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Get_NoCustomerFound_ThrowsException()
        {
            // Arrange
            int expectedCustomernumber = 123456;

            using (var context = new BitLotteryContext(Options))
            {
                CustomerRepository customerRepository = new CustomerRepository(context);

                try
                {
                    // Act
                    customerRepository.Get(expectedCustomernumber);
                }
                catch (Exception exception)
                {
                    // Assert
                    exception.Message.Should().Be("Customernumber: 123456 not found");
                    throw;
                }
            }
        }
    }
}
