using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class CreateCustomerInput 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public float InitialCredit { get; set; }

    }
}
