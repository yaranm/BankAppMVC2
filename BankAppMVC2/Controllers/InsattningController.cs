using BankAppMVC2.Data;
using BankAppMVC2.Services;
using BankAppMVC2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Controllers
{
    public class InsattningController : Controller
    {
        private readonly ITransactionRepository _transaction;
        private readonly IAccountRepository _account;


        public InsattningController(ITransactionRepository transaction, IAccountRepository account)
        {
            _transaction = transaction;
            _account = account;
        }
        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new InsattningViewModel();               

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New (InsattningViewModel viewModel)
        {

            var InAccount = _account.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountId);
            if (InAccount == null)
            {
                ModelState.AddModelError("AccountId", "Ogiltigt konto nr");
            }
            if (viewModel.Amount < 0)
                ModelState.AddModelError("Amount", "Beloppet får inte vara negativt!");


            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transaction.AddTrans(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = "Credit";
                dbTrans.Operation = "Credit in Cash";
                dbTrans.Amount = viewModel.Amount;


                var dbAcc = _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance + viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;

                _transaction.Save();
                return RedirectToAction("New");


            }

                return View(viewModel);
        }

    }
}
