using BankAppMVC2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Services
{
    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetAllTransaction();
        public void AddTrans(Transaction dbTrans);
        public void Save();


    }

    public class TransactionRepository : ITransactionRepository
    {
        protected readonly BankAppDataContext _dbContext;

        public TransactionRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Transaction> GetAllTransaction()
        {
            return _dbContext.Transactions;

        }
        public void AddTrans(Transaction dbTrans)
        {
            _dbContext.Transactions.Add(dbTrans);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
