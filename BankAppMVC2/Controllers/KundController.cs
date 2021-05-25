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
    public class KundController : Controller
    {
        private readonly ICustomerRepository _customer;
        private readonly IAccountRepository _account;

        public KundController(ICustomerRepository customer, IAccountRepository account) 
        {
            _customer = customer;
            _account = account;
        }
       
        public IActionResult Search(string q, int page = 1)
        {
            
            var viewModel = new KundSearchViewModel();

            var query = _customer.GetAllCustomer()
                .Where(r => q == null || r.Surname.Contains(q) || r.Givenname.Contains(q) || r.City.Contains(q));


            int totalRowCount = query.Count();

            int pageSize = 50;

            var pageCount = (double)totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);


            int howManyRecordsToSkip = (page - 1) * pageSize;  // Sida 1 ->  0

            query = query.Skip(howManyRecordsToSkip).Take(pageSize);



            viewModel.Kunder = query
                .Select(e => new KundItemViewModel
                {

                    Givenname = e.Givenname,
                    Surname = e.Surname,
                    Streetaddress = e.Streetaddress,
                    City = e.City,
                    NationalId = e.NationalId,
                    CustomerId = e.CustomerId

                }).ToList();

            viewModel.q = q;
            viewModel.Page = page;
            

            return View(viewModel);
        }

        
        public IActionResult KundBild(int id)
        {
            var viewModel = new KundBildViewModel();

            if (_customer.GetAllCustomer().Include(x => x.Dispositions)
                .FirstOrDefault(r => r.CustomerId == id) == null)
            {
                viewModel.FinnsInte = true;
                return View(viewModel);
            }

            var p = _customer.GetAllCustomer().Include(x => x.Dispositions)
                .First(r => r.CustomerId == id);


            viewModel.CustomerId = p.CustomerId;
            viewModel.Gender = p.Gender;
            viewModel.Givenname = p.Givenname;
            viewModel.Surname = p.Surname;
            viewModel.Streetaddress = p.Streetaddress;
            viewModel.City = p.City;
            viewModel.Zipcode = p.Zipcode;
            viewModel.Country = p.Country;
            viewModel.CountryCode = p.CountryCode;
            viewModel.Birthday = p.Birthday;
            viewModel.NationalId = p.NationalId;
            viewModel.Telephonecountrycode = p.Telephonecountrycode;
            viewModel.Telephonenumber = p.Telephonenumber;
            viewModel.Emailaddress = p.Emailaddress;

            var dispAcc = p.Dispositions.ToList();

            foreach(var d in dispAcc)
            {
                var accout = new KundKontoViewModel();
                var dbacc = _account.GetAllAccount().First(n => n.AccountId.Equals(d.AccountId));
                accout.AccountId = dbacc.AccountId;
                accout.Balance = dbacc.Balance;
                accout.Created = dbacc.Created;
                accout.Frequency = dbacc.Frequency;

                    viewModel.Konto.Add(accout);
            }
            viewModel.SumOfCustomerAccounts = viewModel.Konto.Sum(x => x.Balance);
            
            return View(viewModel);

        }



        [HttpGet]
        [Authorize(Roles = "Cashier, Admin")]
        public IActionResult Edit(int id)
        {
            var viewModel = new KundEditViewModel();

            var p = _customer.GetAllCustomer()
                .First(r => r.CustomerId == id);


            viewModel.CustomerId = p.CustomerId;
            viewModel.Gender = p.Gender;
            viewModel.Givenname = p.Givenname;
            viewModel.Surname = p.Surname;
            viewModel.Streetaddress = p.Streetaddress;
            viewModel.City = p.City;
            viewModel.Zipcode = p.Zipcode;
            viewModel.Country = p.Country;
            viewModel.CountryCode = p.CountryCode;
            viewModel.Birthday = p.Birthday;
            viewModel.NationalId = p.NationalId;
            viewModel.Telephonecountrycode = p.Telephonecountrycode;
            viewModel.Telephonenumber = p.Telephonenumber;
            viewModel.Emailaddress = p.Emailaddress;

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Cashier, Admin")]
        public IActionResult Edit (int id, KundEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var p = _customer.GetAllCustomer()
               .First(r => r.CustomerId == id);

                p.CustomerId = model.CustomerId;
                p.Gender = model.Gender;
                p.Givenname = model.Givenname;
                p.Surname = model.Surname;
                p.Streetaddress = model.Streetaddress;
                p.City = model.City;
                p.Zipcode = model.Zipcode;
                p.Country = model.Country;
                p.CountryCode = model.CountryCode;
                p.Birthday = model.Birthday;
                p.NationalId = model.NationalId;
                p.Telephonecountrycode = model.Telephonecountrycode;
                p.Telephonenumber = model.Telephonenumber;
                p.Emailaddress = model.Emailaddress;

                

                _customer.Save();
                return RedirectToAction("Search");
            }

            return View(model);
        }
        [Authorize(Roles = "Cashier, Admin")]
        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new NewKundViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New(int id, NewKundViewModel viewModel)
        {
            

            if (ModelState.IsValid)
            {
                var dbCustomer = new Customer();

                _customer.Addcustomer(dbCustomer);

                

                dbCustomer.Gender = viewModel.Gender;
                dbCustomer.Givenname = viewModel.Givenname;
                dbCustomer.Surname = viewModel.Surname;
                dbCustomer.Streetaddress = viewModel.Streetaddress;
                dbCustomer.City = viewModel.City;
                dbCustomer.Zipcode = viewModel.Zipcode;
                dbCustomer.Country = viewModel.Country;
                dbCustomer.CountryCode = viewModel.CountryCode;
                dbCustomer.Birthday = viewModel.Birthday;
                dbCustomer.NationalId = viewModel.NationalId;
                dbCustomer.Telephonecountrycode = viewModel.Telephonecountrycode;
                dbCustomer.Telephonenumber = viewModel.Telephonenumber;
                dbCustomer.Emailaddress = viewModel.Emailaddress;



                _customer.Save();
                return RedirectToAction("Search");
            }
            return View(viewModel);
        }


        [Authorize(Roles = "Cashier, Admin")]
        //[HttpDelete]
        public IActionResult Delete(int id)
        {
                var DeleteCustom = _customer.GetAllCustomer()
               .First(r => r.CustomerId == id);

                _customer.DeleteCustomer(DeleteCustom);

                _customer.Save();

            return RedirectToAction("Search");
        }
    }
}
