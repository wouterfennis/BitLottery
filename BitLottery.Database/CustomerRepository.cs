using BitLottery.Database.Interfaces;
using BitLottery.Models;
using System;
using System.Linq;

namespace BitLottery.Database
{
    public class CustomerRepository : ICustomerRepository 
    {
        private readonly BitLotteryContext _context;

        public CustomerRepository(BitLotteryContext context)
        {
            _context = context;
        }

        public bool Delete(int key)
        {
            bool deleteSuccessfull = false;

            Customer foundCustomer = Get(key);
            if (foundCustomer != null)
            {
                _context.Customers.Remove(foundCustomer);
                _context.SaveChanges();
                deleteSuccessfull = true;
            }
            return deleteSuccessfull;
        }

        public Customer Get(int key)
        {
            return _context.Customers
                .SingleOrDefault(customer => customer.Id == key);
        }

        public int Insert(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
            return entity.Number;
        }

        public bool Update(Customer entity, int key)
        {
            bool isUpdateSuccessfull = false;
            Customer foundCustomer = Get(key);
            if (foundCustomer != null)
            {
                foundCustomer = entity;
                _context.SaveChanges();
                isUpdateSuccessfull = true;
            }
            return isUpdateSuccessfull;
        }
    }
}
