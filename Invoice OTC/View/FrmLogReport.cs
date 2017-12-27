using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using InvoiceOTC.Model;
//using InvoiceOTC.Repository.Service;
//using InvoiceOTC.Repository.API;

namespace Invoice_OTC.View
{
    public partial class FrmLogReport : Form
    {
        #region Declaration
        //private SortableBindingList<Logs> m_LogList;
        //private Log4NetRepository rep;
        #endregion

        public FrmLogReport()
        {
            InitializeComponent();
        }

        private void FrmLogReport_Load(object sender, EventArgs e)
        {
            //rep = new Log4NetRepository();

            //m_LogList = rep.GetSortedList();
            //logsBindingSource.DataSource = m_LogList;
        }
    }
}
