﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_OTC.Controller
{
    public abstract class Command
    {
        //Method
        public abstract object Execute();       
    }
}
