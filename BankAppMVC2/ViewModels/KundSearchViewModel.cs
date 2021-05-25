using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class KundSearchViewModel
    {
        
        public List<KundItemViewModel> Kunder { get; set; } = new List<KundItemViewModel>();
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public string q { get; set; }

    }
}
