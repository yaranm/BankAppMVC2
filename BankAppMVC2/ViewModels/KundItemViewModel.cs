﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class KundItemViewModel
    {
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Streetaddress { get; set; }
        public string NationalId { get; set; }
        public int CustomerId { get; set; }
    }
}
