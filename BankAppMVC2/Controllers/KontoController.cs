using BankAppMVC2.Data;
using BankAppMVC2.Services;
using BankAppMVC2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Controllers
{
    public class KontoController : Controller
    {
        private readonly IAccountRepository _account;
        private readonly ITransactionRepository _transaction;


        public KontoController(BankAppDataContext dbContext, IAccountRepository account, ITransactionRepository transaction) 
        {
            _account = account;
            _transaction = transaction;
        }

        public IActionResult KontoBild(int id)
        {
            var viewModel = new KontoBildViewModel();
            var w = _account.GetAllAccount().Include(x => x.Transactions)
                .First(r => r.AccountId == id);

            viewModel.AccountId = w.AccountId; 
            viewModel.Balance = w.Balance;
            viewModel.Trans = w.Transactions.OrderByDescending(y => y.TransactionId).Skip(0).Take(20).Select(p => new KontoTransViewModel
            {

                TransactionId = p.TransactionId,
                AccountId = p.AccountId,
                Date = p.Date,
                Type = p.Type,
                Operation = p.Operation,
                Amount = p.Amount,
                Balance = p.Balance,
                Symbol = p.Symbol,
                Bank = p.Bank,
                Account = p.Account
            }).ToList();

            return View(viewModel);
        }

        public IActionResult TransFler (int id,int skip )
        {
            var viewModel = new KontoTransListaViewModel();
            var w = _account.GetAllAccount().Include(x => x.Transactions)
               .First(r => r.AccountId == id);
            
            viewModel.Trans = w.Transactions.OrderByDescending(y => y.TransactionId).Skip(skip).Take(20).Select(p => new KontoTransViewModel
            {

                TransactionId = p.TransactionId,
                AccountId = p.AccountId,
                Date = p.Date,
                Type = p.Type,
                Operation = p.Operation,
                Amount = p.Amount,
                Balance = p.Balance,
                Symbol = p.Symbol,
                Bank = p.Bank,
                Account = p.Account
            }).ToList();


            return View(viewModel);

        }

        [Authorize(Roles = "Cashier, Admin")]
        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new NewKontoViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New (NewKontoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbAccount = new Account();
                _account.AddAccount(dbAccount);

                dbAccount.Balance = viewModel.Balance;
                dbAccount.Created = viewModel.Created;
                dbAccount.Frequency = viewModel.Frequency;

                _account.Save();
                return RedirectToAction("TransFler");

            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Cashier, Admin")]
        public IActionResult Edit(int id)
        {
            var viewModel = new EditKontoViewModel();

            var p = _account.GetAllAccount()
                .First(r => r.AccountId == id);

            viewModel.Balance = p.Balance;
            viewModel.Created = p.Created;
            viewModel.Frequency = p.Frequency;

            return View(viewModel);
        }


        [Authorize(Roles = "Cashier, Admin")]
        //[HttpDelete]
        public IActionResult Delete(int id)
        {
            var DeleteAccount = _account.GetAllAccount()
           .First(r => r.AccountId == id);

            _account.DeleteAcc(DeleteAccount);

            _account.Save();

            return RedirectToAction("Search");
        }
    }
}
