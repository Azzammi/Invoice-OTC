﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_OTC.Controller
{
    public class AppController
    {
        #region Public Method

        public object ExecuteCommand(Command command)
        {
            return command.Execute();
        }

        #endregion
    }
}
