using BitLottery.Api.Models;

namespace BitLottery.Api.Controllers.Interfaces
{
    public interface ICustomerController
    {
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer">The customer information</param>
        /// <returns>The new customer number</returns>
        int CreateCustomer(CustomerInfo customerInfo);
    }
}