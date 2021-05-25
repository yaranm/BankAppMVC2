using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class OverforingViewModel
    {
        public int TransactionId { get; set; }
        [Required]
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        [Required]
       //[Remote("Overskridning", "Uttag", AdditionalFields = "AccountId")]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public int AccountIdMot { get; set; }
    }
}
