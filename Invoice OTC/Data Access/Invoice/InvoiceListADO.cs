﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Invoice_OTC.Model;
using Invoice_OTC.Controller;

namespace Invoice_OTC.Data_Access
{
    class InvoiceListADO
    {
        #region Decalratios

        #endregion

        #region Constructor
        public InvoiceListADO()
        {

        }
        #endregion

        #region Methods

        public void LoadInvoiceList(InvoiceList invoiceList)
        {
            //Build query to get Invoice's and their roti
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(String.Format("Select invoiceID, noInvoice, dueDate, invoiced.outletCode, OUTLET.OUTLNAME, subTotal, ppn, total, issuedDate, isPPN, nomorPO, Periode, Pengguna, id_payment, isPayed FROM invoiced INNER JOIN OUTLET ON invoiced.outletCode = OUTLET.OUTLCODE ;"));
            sqlQuery.Append(string.Format("Select rotiID, invoiceID, itemCode, itemQty, itemPrice, discount, subTotal FROM invoiceDetail"));

            //Get a data set from the query
            DataSet dataSet = DataProvider.GetDataSet(sqlQuery.ToString());

            //Create Variables for data set tables
            DataTable invoiceTable = dataSet.Tables[0];
            DataTable detailTable = dataSet.Tables[1];

            //Create a data relation from invoice (parent table) to detail Invoice
            DataColumn parentColumn = invoiceTable.Columns["invoiceID"];
            DataColumn childColumn = detailTable.Columns["invoiceID"];
             DataRelation invoiceToDetail = new DataRelation("invoiceToDetail", parentColumn, childColumn, false);
            dataSet.Relations.Add(invoiceToDetail);

            //Load InvoiceList from the data set
            InvoiceItem nextInvoice = null;
            rotiItem nextRoti = null;
            foreach(DataRow parentRow in invoiceTable.Rows)
            {
                //Create a new invoice
                bool createDatabaseRecord = false;
                nextInvoice = new InvoiceItem(createDatabaseRecord);

                //Fill in invoice properties
                nextInvoice.InvoiceID = Convert.ToInt32(parentRow["invoiceID"]);
                nextInvoice.Nomor = parentRow["noInvoice"].ToString();
                nextInvoice.DueDate = Convert.ToDateTime(parentRow["dueDate"]);
                nextInvoice.OutletCode = parentRow["outletCode"].ToString();
                
                //nextInvoice.SubTotal = Convert.ToDecimal(parentRow["subTotal"]);
                nextInvoice.PPN = Convert.ToInt32(parentRow["ppn"]);
                //nextInvoice.Total = Convert.ToDecimal(parentRow["total"]);
                nextInvoice.IssuedData = Convert.ToDateTime(parentRow["issuedDate"]);
                nextInvoice.IsPPN = Convert.ToBoolean(parentRow["isPPN"]);
                nextInvoice.NomorPO = parentRow["nomorPO"].ToString();
                nextInvoice.User = parentRow["pengguna"].ToString();

                //Get Invoice Item
                DataRow[] childRows = parentRow.GetChildRows(invoiceToDetail);

                //Create invoiceItem object foe each of the invoice
                foreach(DataRow childRow in childRows)
                {
                    //Create a new item
                    nextRoti = new rotiItem();

                    //Fill in roti's properties
                    nextRoti.ID = Convert.ToInt32(childRow["rotiID"]);
                    nextRoti.Invoiceid = Convert.ToInt32(childRow["invoiceID"]);                
                    nextRoti.ItemCode = childRow["itemCode"].ToString();                   
                    nextRoti.Qty = Convert.ToInt32(childRow["itemQty"]);
                    nextRoti.Price = Convert.ToDecimal(childRow["itemPrice"]);
                    nextRoti.Discount = Convert.ToDouble(childRow["discount"]);
                    //nextRoti.SubTotal = Convert.ToDecimal(childRow["Subtotal"]);

                    //Add roti to invoice
                    if(nextRoti.ItemCode != "0")
                    {
                        nextInvoice.Items.Add(nextRoti);
                    }
                    else
                    {
                        nextRoti.DeleteDatabaseRecord();
                    }                    
                }

                //Add the invoice to the invoice List                
                invoiceList.Add(nextInvoice);
            }

            //Dispose of the dataset
            dataSet.Dispose();
        }

        #endregion
    }
}
