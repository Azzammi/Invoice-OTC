using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Invoice_OTC.View;

//using InvoiceOTC.Repository.API;
//using InvoiceOTC.Repository.Service;
//using InvoiceOTC.Model;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Invoice_OTC
{
    static class Program
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new RibbonForm1());
            //this source : https://www.codeproject.com/Articles/3014/Single-Process-Instance-Object
            //GlobalContext.Properties["UserName"] = "Admin";
            //GlobalContext.Properties["LogDate"] = "Apak";
            //log.Info("Log Greget");
            using (SingleProgramInstance spi = new SingleProgramInstance("x5k6yz"))
            {
                if (spi.IsSingleInstance)
                {
                    Application.Run(new RibbonForm1());
                }
                else
                {
                    spi.RaiseOtherProcess();
                }
            }
        }
    }
}
