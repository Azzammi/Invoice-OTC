﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Invoice_OTC.Model;
using Invoice_OTC.Controller;
using Invoice_OTC.Controller.Salesman;
using Invoice_OTC.Controller.Outlet;
using System.Windows.Forms;

namespace Invoice_OTC.View
{
    public partial class FrmDetailedSlsman : Form
    {
        #region Declaration
        AppController m_AppController;
        SalesmanList m_SalesmanList;
        SalesmanItem m_Item;

        outletList m_OutletList;
        #endregion

        public FrmDetailedSlsman()
        {
            InitializeComponent();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void salesmanItemBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (m_SalesmanList == null) return;

            SalesmanItem item = (SalesmanItem)salesmanItemBindingSource.Current;
            commandCRUSalesman cruSalesman = new commandCRUSalesman(item);
            m_AppController.ExecuteCommand(cruSalesman);
             
        }

        private void FrmDetailedSlsman_Load(object sender, EventArgs e)
        {
            m_AppController = new AppController();

            CommandGetSalesman getSalesman = new CommandGetSalesman();
            m_SalesmanList = (SalesmanList)m_AppController.ExecuteCommand(getSalesman);            

            salesmanItemBindingSource.DataSource = m_SalesmanList;
        }

        private void browsePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog opd = new OpenFileDialog())
            {
                switch (opd.ShowDialog())
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.OK:                        
                        m_Item = (SalesmanItem)salesmanItemBindingSource.Current;
                        m_Item.SlsmPhoto = opd.FileName;

                        salesmanItemBindingSource.ResetCurrentItem();
                        break;
                }
            }            
        }

        private void salesmanItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (m_SalesmanList == null) return;

            m_Item = (SalesmanItem)salesmanItemBindingSource.Current;
            if (m_Item == null) return;

            CommandGetOutletBySalesman getOutlet = new CommandGetOutletBySalesman(m_Item.SlsmCode);
            m_OutletList = (outletList)m_AppController.ExecuteCommand(getOutlet);

            outletItemBindingSource.DataSource = m_OutletList;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (m_SalesmanList == null) return;
            m_Item = (SalesmanItem)salesmanItemBindingSource.Current;
            if (m_Item == null) return;

            var pesan = MessageBox.Show("Apakah Anda Yakin Ingin Menghapus " + m_Item.SlsmName + " ? ", "Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (pesan == DialogResult.Yes)
            {
                CommandDeleteSalesman deleteSalesman = new CommandDeleteSalesman(m_SalesmanList, m_Item);
                m_AppController.ExecuteCommand(deleteSalesman);
            }                
        }

        /// <summary>
        /// Overrid Method to setting the shortcut keys
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.N))
            {
                bindingNavigatorAddNewItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                salesmanItemBindingNavigatorSaveItem.PerformClick();
                return true;
            }
            else if (keyData == (Keys.Delete))
            {
                bindingNavigatorDeleteItem.PerformClick();
                return true;
            }           

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
