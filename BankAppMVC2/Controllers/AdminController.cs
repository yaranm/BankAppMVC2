using BankAppMVC2.Data;
using BankAppMVC2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAccountRepository _account;
        private readonly ITransactionRepository _transaction;


        public AdminController(BankAppDataContext dbContext, IAccountRepository account, ITransactionRepository transaction) : base(dbContext)
        {
            _account = account;
            _transaction = transaction;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
