using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class KontoBildViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        public List<KontoTransViewModel> Trans { get; set; } = new List<KontoTransViewModel>();
    }
}
