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
    public class OverforingController : Controller
    {

        private readonly ITransactionRepository _transaction;
        private readonly IAccountRepository _account;

        public OverforingController(ITransactionRepository transaction, IAccountRepository account) 
        {
            _transaction = transaction;
            _account = account;
        }

        [HttpGet]
        public IActionResult NewOverforing()
        {
            var viewModel = new OverforingViewModel();
            return View(viewModel);
        }

       


            [HttpPost]
        public IActionResult NewOverforing(OverforingViewModel viewModel)
        {

            var sandare = _account.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountId);
           if (sandare == null)
           {
                ModelState.AddModelError("AccountId", "Ogiltigt konto nr");
           }

            var mottagare = _account.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountIdMot);
            if (mottagare == null)
            {
                ModelState.AddModelError("AccountIdMot", "Ogiltigt konto nr");
            }

            if (viewModel.Amount < 0)
                ModelState.AddModelError("Amount", "Beloppet får inte vara negativt!");

            if (sandare==null||_account.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountId).Balance < viewModel.Amount)
                ModelState.AddModelError("Amount", "Det finns inte så mycket på kontot! Eller Ogiltigt konto nr!");


            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transaction.AddTrans(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = "Debit";
                dbTrans.Operation = "Transaction to Another Account";
                dbTrans.Amount = decimal.Negate(viewModel.Amount);
                

                var dbAcc = _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                dbAcc.Balance = dbAcc.Balance - viewModel.Amount;

                var dbMottag = _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountIdMot);
                dbMottag.Balance = dbMottag.Balance + viewModel.Amount;

                var dbTransMot = new Transaction();
                _transaction.AddTrans(dbTransMot);
                dbTransMot.AccountId = viewModel.AccountIdMot;
                dbTransMot.Date = DateTime.Now;
                dbTransMot.Type = "Credit";
                dbTransMot.Operation = "Collection from Another Account";
                dbTransMot.Amount = viewModel.Amount;

                dbTrans.Balance = dbAcc.Balance;
                dbTransMot.Balance = dbMottag.Balance;


                _transaction.Save();
                return RedirectToAction("NewOverforing");


            }

            return View(viewModel);
        }
    }
}
