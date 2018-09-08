using BitLottery.Api.Controllers.Interfaces;
using BitLottery.Api.Models;
using BitLottery.Database.Interfaces;
using BitLottery.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitLottery.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller, ICustomerController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public int CreateCustomer(CustomerInfo customerInfo)
        {
            var customer = new Customer
            {
                Name = customerInfo.Name
            };
            int customerNumber = _customerRepository.Insert(customer);
            return customerNumber;
        }

        [HttpGet("{customerNumber}")]
        public Customer GetCustomer(int customerNumber)
        {
            Customer customer = _customerRepository.Get(customerNumber);
            return customer;
        }

        [HttpPut]
        public bool UpdateCustomer(Customer newCustomer, int customerNumber)
        {
            bool wasUpdateSuccesfull = _customerRepository.Update(newCustomer, customerNumber);
            return wasUpdateSuccesfull;
        }
    }
}
