using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class KontoTransListaViewModel
    {
        public List<KontoTransViewModel> Trans { get; set; } = new List<KontoTransViewModel>();
    }
}
