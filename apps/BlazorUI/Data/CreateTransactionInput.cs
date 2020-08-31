using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class CreateTransactionInput
    {
        public string CustomerId { get; set; }
        public string AccountId { get; set; }
        public float Amount { get; set; }
    }
}
