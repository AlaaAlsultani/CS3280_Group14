using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_Group14
{
    /// <summary>
    /// Class to run SQL Queries on the Database
    /// </summary>
    class clsDBQueries
    {
        /// <summary>
        /// Database connection object
        /// </summary>
        private clsDataAccess db;
        /// <summary>
        /// Item Object to hold currently selected item
        /// </summary>
        private clsItem currentItem;
        /// <summary>
        /// Invoice Object to hold currently selected invoice
        /// </summary>
        private clsInvoice currentInvoice;
        /// <summary>
        /// Object for SQL statements
        /// </summary>
        private clsSQL sql;

        /// <summary>
        /// Class Constructor
        /// </summary>
        public clsDBQueries()
        {
            try
            {
                //Initialize Data access and SQL objects
                db = new clsDataAccess();
                sql = new clsSQL();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #region Main Page Queries
        /// <summary>
        /// Get all Items from the database
        /// </summary>
        /// <returns>Returns list of clsItem</returns>
        public List<clsItem> GetItems()
        {
            try
            {
                //Method Variables
                List<clsItem> items = new List<clsItem>();
                string sSQL = sql.SelectAllItems();
                int iNumReturned = 0;

                //Execute Query and return data as dataset
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref iNumReturned);

                //Loop Through Dataset to fill "items" with list of each item in database
                for(int i=0; i < iNumReturned; ++i)
                {
                    currentItem = new clsItem();
                    currentItem.Code = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                    currentItem.Description = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    currentItem.Cost = (decimal)ds.Tables[0].Rows[i]["Cost"];

                    items.Add(currentItem);
                }

                //Return List of Items
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add a new invoice to the database
        /// </summary>
        /// <param name="newInvoice">Invoice Object to add</param>
        /// <returns>New Invoice's Number</returns>
        public string AddInvoice(clsInvoice newInvoice)
        {
            try
            {
                string sSQL = sql.AddInvoice(newInvoice.InvoiceDate, newInvoice.InvoiceTotal);

                //Insert new invoice into Invoices table
                int iNumReturned = db.ExecuteNonQuery(sSQL);

                //Get new invoice number
                string newInvoiceNumber = GetNewestInvoiceNumber();

                //Insert Each Line Item
                for(int i=0; i < newInvoice.Items.Count; ++i)
                {
                    sSQL = sql.AddLineItem(newInvoiceNumber, $"{i + 1}", newInvoice.Items[i].Code);
                    iNumReturned = db.ExecuteNonQuery(sSQL);
                }

                return newInvoiceNumber;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get the newest invoice number in the database
        /// </summary>
        /// <returns>Returns a string of the newest invoice's number</returns>
        public string GetNewestInvoiceNumber()
        {
            try
            {
                string sSQL = sql.GetNewestInvoice();
                return db.ExecuteScalarSQL(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an invoice currently in the database
        /// </summary>
        /// <param name="invoice">Invoice object with all changes</param>
        public void UpdateInvoice(clsInvoice invoice)
        {
            try
            {
                string sSQL = sql.UpdateInvoice(invoice.InvoiceNumber, invoice.InvoiceDate, invoice.InvoiceTotal);

                //Update Invoice Table
                int iNumReturned = db.ExecuteNonQuery(sSQL);

                //Delete All Current Line Items to replace them later
                sSQL = sql.DeleteInvoiceLineItems(invoice.InvoiceNumber);
                iNumReturned = db.ExecuteNonQuery(sSQL);

                //Insert Each Line Item as new
                for (int i = 0; i < invoice.Items.Count; ++i)
                {
                    sSQL = sql.AddLineItem(invoice.InvoiceNumber, $"{i + 1}", invoice.Items[i].Code);
                    iNumReturned = db.ExecuteNonQuery(sSQL);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an existing invoice from the database
        /// </summary>
        /// <param name="invoiceNumber">Invoice number of invoice to be deleted</param>
        public void DeleteInvoice(string invoiceNumber)
        {
            try
            {
                string sSQL = sql.DeleteInvoiceLineItems(invoiceNumber);

                //Delete Invoice Line Items
                int iNumReturned = db.ExecuteNonQuery(sSQL);

                //Delete Invoice
                sSQL = sql.DeleteInvoice(invoiceNumber);
                iNumReturned = db.ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// Execute query to get invoice numbers and put them into a list
        /// </summary>
        /// <param name="sBegin">BEgin date</param>
        /// <param name="sEnd">End date</param>
        /// <returns>List of invoice numbers</returns>
        public List<int> GetInvoiceNumbersByDateRange(string sBegin, string sEnd)
        {
            try
            {
                List<int> lstInvoiceNum = new List<int>();

                int iNumReturned = 0;

                //create data set and execute sql statement
                DataSet ds = db.ExecuteSQLStatement(sql.GetInvoiceNumbersByDateRange(sBegin,sEnd), ref iNumReturned);

                //loop through data set and set returns to string then to int and add to list
                for (int i = 0; i < iNumReturned; i++)
                {
                    lstInvoiceNum.Add(Int32.Parse(ds.Tables[0].Rows[i]["InvoiceNum"].ToString()));
                }

                return lstInvoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Execute query to get all item descriptions into a list
        /// </summary>
        /// <returns>List of item descriptions</returns>
        public List<string> GetItemsDesc()
        {
            try
            {
                List<string> lstItems = new List<string>();

                int iNumReturned = 0;

                //create data set and execute sql statement
                DataSet ds = db.ExecuteSQLStatement(sql.GetItems(), ref iNumReturned);

                //loop through data set and set returns to string then to int and add to list
                for (int i = 0; i < iNumReturned; i++)
                {
                    lstItems.Add(ds.Tables[0].Rows[i]["ItemDesc"].ToString());
                }

                return lstItems;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Get all invoices that containg selected item
        /// </summary>
        /// <param name="sItemCode">Item code of current item</param>
        /// <returns>data set containing invoices</returns>
        public DataSet GetInvoicesContainingItem(string sItemCode)
        {
            try
            {
                int iNumReturned = 0;

                //create data set and execute sql statement
                DataSet ds = db.ExecuteSQLStatement(sql.GetInvoicesContainingItem(sItemCode), ref iNumReturned);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
