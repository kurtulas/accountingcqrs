﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Data
{
    public class CreateAccountInput
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }

        public float InitialCredit { get; set; }

    }
}
