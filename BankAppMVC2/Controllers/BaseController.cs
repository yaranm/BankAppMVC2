using BankAppMVC2.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankAppMVC2.Controllers
{
    public class BaseController : Controller
    {
        protected readonly BankAppDataContext _dbContext;

        public BaseController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}