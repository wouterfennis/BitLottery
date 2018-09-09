using BitLottery.Api.Controllers;
using BitLottery.Api.Models;
using BitLottery.Database.Interfaces;
using BitLottery.Entities.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BitLottery.Api.UnitTests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        private Mock<ICustomerRepository> customerRepositoryMock;
        private CustomerController customerController;

        [TestInitialize]
        public void Initialize()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>(MockBehavior.Strict);

            customerController = new CustomerController(customerRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateCustomer_CallsRepositoryLayer_ReturnsCorrectNumber()
        {
            // Arrange
            int expectedNumber = 12345;
            string expectedName = "Cliff Bellman";
            var expectedCustomer = new Customer
            {
                Name = expectedName,
            };

            Customer actualCustomer = null;
            customerRepositoryMock
                .Setup(mock => mock.Insert(It.IsAny<Customer>()))
                .Callback((Customer customer) =>
                {
                    actualCustomer = customer;
                })
                .Returns(expectedNumber);

            var customerInfo = new CustomerInfo
            {
                Name = expectedName
            };

            // Act
            var result = customerController.CreateCustomer(customerInfo);

            // Assert
            result.Should().Be(expectedNumber);
            actualCustomer.Should().NotBeNull();
            actualCustomer.Name.Should().Be(expectedName);

            customerRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetCustomer_CallsRepositoryLayer_ReturnsCorrectCustomer()
        {
            // Arrange
            int expectedNumber = 12345;
            var expectedCustomer = new Customer
            {
                Number = expectedNumber,
                Name = "Cliff Bellman",
            };

            customerRepositoryMock
                .Setup(mock => mock.Get(expectedNumber))
                .Returns(expectedCustomer);


            // Act
            var result = customerController.GetCustomer(expectedNumber);

            // Assert
            result.Should().Be(expectedCustomer);

            customerRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCustomer_CallsRepositoryLayer_ReturnsCorrectCustomer()
        {
            // Arrange
            int expectedNumber = 12345;
            var expectedCustomer = new Customer
            {
                Number = expectedNumber,
                Name = "Cliff Bellman",
            };

            customerRepositoryMock
                .Setup(mock => mock.Update(expectedCustomer, expectedNumber))
                .Returns(true);

            // Act
            var result = customerController.UpdateCustomer(expectedCustomer, expectedNumber);

            // Assert
            result.Should().BeTrue();

            customerRepositoryMock.VerifyAll();
        }
    }
}
