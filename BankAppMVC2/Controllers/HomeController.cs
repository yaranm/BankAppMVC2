using BankAppMVC2.Data;
using BankAppMVC2.Models;
using BankAppMVC2.Services;
using BankAppMVC2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _account;
        private readonly ICustomerRepository _customer;

        public HomeController(ILogger<HomeController> logger, BankAppDataContext dbContext, IAccountRepository account, ICustomerRepository customer)
        {
            _logger = logger;
            _account = account;
            _customer = customer;
        }
        [ResponseCache(Duration = 30)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.AntalKonton = _account.GetAllAccount().Count();
            viewModel.AntalKunder = _customer.GetAllCustomer().Count();
            viewModel.TotalSummaAllaKonton = _account.GetAllAccount().Sum(r => r.Balance);
            viewModel.SenastUppdaterad = DateTime.Now;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
