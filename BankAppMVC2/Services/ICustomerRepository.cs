using BankAppMVC2.Data;
using BankAppMVC2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Services
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomer();
        public void Addcustomer(Customer dbCustomer);
        public void DeleteCustomer(Customer DeleteCustom);
        public void Save();
       

    }

    class CustomerRepository : ICustomerRepository
    {
        protected readonly BankAppDataContext _dbContext;

        public CustomerRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetAllCustomer()
        {
            return _dbContext.Customers;

        }
        public void Addcustomer(Customer dbCustomer)
        {
            _dbContext.Customers.Add(dbCustomer);
        }
        public void DeleteCustomer(Customer DeleteCustom)
        {
            _dbContext.Customers.Remove(DeleteCustom);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
