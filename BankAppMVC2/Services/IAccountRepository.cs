using BankAppMVC2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Services
{
    public interface IAccountRepository
    {
        IQueryable<Account> GetAllAccount();
        public void AddAccount(Account dbAccount);
        public void DeleteAcc(Account DeleteAccount);
        public void Save();
       
    }

    public class AccountRepository : IAccountRepository
    {
        protected readonly BankAppDataContext _dbContext;

        public AccountRepository(BankAppDataContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IQueryable<Account> GetAllAccount()
        {
           return _dbContext.Accounts;
                        
        }
        public void AddAccount(Account dbAccount)
        {
            _dbContext.Accounts.Add(dbAccount);
        }
        public void DeleteAcc(Account DeleteAccount)
        {
            _dbContext.Accounts.Remove(DeleteAccount);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        
    }
}
