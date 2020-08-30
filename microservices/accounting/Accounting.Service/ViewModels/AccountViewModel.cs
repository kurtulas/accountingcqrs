using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Service.ViewModels
{
    public class AccountViewModel
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }

        public float InitialCredit { get; set; }
    }
}
