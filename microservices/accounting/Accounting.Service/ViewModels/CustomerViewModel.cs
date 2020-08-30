using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Service.ViewModels
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public float InitialCredit { get; set; }

    }
}
