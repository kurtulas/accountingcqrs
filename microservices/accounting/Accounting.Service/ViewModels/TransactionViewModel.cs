using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Service.ViewModels
{
    public class TransactionViewModel
    {
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public float Amount { get; set; }
    }
}
