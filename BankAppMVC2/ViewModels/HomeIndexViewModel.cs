using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class HomeIndexViewModel
    {
        public int AntalKunder { get; set; }

        public int AntalKonton { get; set; }

        public decimal TotalSummaAllaKonton { get; set; }

        public DateTime SenastUppdaterad { get; set; }
    }
}
