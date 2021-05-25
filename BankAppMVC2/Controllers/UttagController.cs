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
    public class UttagController : Controller
    {
        private readonly ITransactionRepository _transaction;
        private readonly IAccountRepository _account;

        
        public UttagController(ITransactionRepository transaction, IAccountRepository account) 
        {
            _transaction = transaction;
            _account = account;
        }

        

        [HttpGet]
        public IActionResult NewUttag()
        {
            var viewModel = new UttagViewModel();
                               

            return View(viewModel);
        }
        /*
        [HttpGet][HttpPost]
        public IActionResult Overskridning(int AccountId, decimal Amount)
        {
            /*  
             *  [Bind(Prefix ="UttagViewModel.AccountId")]
            if (_account.GetAllAccount().First(r => r.AccountId == AccountId).Balance < Amount)
            {
                return Json("Det finns inte så mycket på kontot!");
            }
                return Json(true);
        }
        */

        [HttpPost]
        public IActionResult NewUttag(UttagViewModel viewModel)
        {
            var UttagAccount = _account.GetAllAccount().FirstOrDefault(r => r.AccountId == viewModel.AccountId);
            if (UttagAccount == null)
            {
                ModelState.AddModelError("AccountId", "Ogiltigt konto nr");
            }
            if(viewModel.Amount <0)
                ModelState.AddModelError("Amount", "Beloppet får inte vara negativt!");

            if (UttagAccount == null || _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountId).Balance < viewModel.Amount)
                ModelState.AddModelError("Amount", "Det finns inte så mycket på kontot! Eller Ogiltigt konto nr!");

            if (ModelState.IsValid)
            {
                var dbTrans = new Transaction();
                _transaction.AddTrans(dbTrans);
                dbTrans.AccountId = viewModel.AccountId;
                dbTrans.Date = DateTime.Now;
                dbTrans.Type = "Debit";
                dbTrans.Operation = "Withdrawal in Cash";
                dbTrans.Amount = decimal.Negate(viewModel.Amount);


                var dbAcc = _account.GetAllAccount().First(r => r.AccountId == viewModel.AccountId);
                var balance = dbAcc.Balance - viewModel.Amount;

                dbTrans.Balance = balance;
                dbAcc.Balance = balance;


                

                _transaction.Save();
                return RedirectToAction("NewUttag");


            }

            return View(viewModel);
        }

    }
}
