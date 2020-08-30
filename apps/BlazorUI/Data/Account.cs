using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        public float Balance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
