﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invoice_OTC.Data_Access;
using FSCollections;

namespace Invoice_OTC.Model
{
    public class rotiItem : FSBindingItem
    {
        #region Declarations

        //Property Variables
        private int p_ID;
        private int invoiceID;
        private string itemCode;        
        private int itemQty;
        private double discount;
        private decimal itemPrice;
        private decimal subTotal;

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor for data 
        /// </summary>
        
        public rotiItem()
        {

        }

        //Use this to create new data
        public rotiItem(int parentID)
        {            
              rotiItemDAO dao = new rotiItemDAO();
              dao.CreateDatabaseRecord(this, parentID);            
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return p_ID; }
            set { p_ID = value; }
        }

        public int Invoiceid
        {
            get { return invoiceID; }
            set { invoiceID = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public int Qty
        {
            get { return itemQty; }
            set { itemQty = value; }
        }

        public decimal Price
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal SubTotal
        {
            get {
                subTotal = itemPrice * itemQty;
                decimal kenaDiskon = (itemPrice * itemQty) * (decimal)discount / 100;
                subTotal += kenaDiskon;
                return subTotal;
            }
            set { subTotal = value; }
        }

        #endregion

        #region Method

        internal void DeleteDatabaseRecord()
        {
            rotiItemDAO dao = new rotiItemDAO();
            dao.DeleteDatabaseRecord(this.p_ID);
        }

        internal void UpdateDatabaseRecord()
        {
            rotiItemDAO dao = new rotiItemDAO();
            dao.UpdateDatabaseRecord(this);
        }

        #endregion
    }
}
