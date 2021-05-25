using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppMVC2.ViewModels
{
    public class UttagViewModel
    {

        public int TransactionId { get; set; }
        
        //[Range(1, int.MinValue, ErrorMessage = "Välj från vilket konto uttaget skall göras ifrån")]
        public int AccountId { get; set; }
        [Required]
        //[Range(1, Int32.MaxValue, ErrorMessage = "Beloppet kan inte vara negativt")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
