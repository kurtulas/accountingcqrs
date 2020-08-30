using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.Service.ViewModels
{
    public class TransactionViewModel
    {
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public float Amount { get; set; }
    }
}
