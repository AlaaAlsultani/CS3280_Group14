using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_Group14
{
    /// <summary>
    /// Class to Store all SQL statements
    /// </summary>
    class clsSQL
    {

        #region Main Window SQL
        /// <summary>
        /// Add a new invoice to the database
        /// </summary>
        /// <param name="invoiceDate">New Invoice's Date</param>
        /// <param name="invoiceTotal">New Invoice's TotalCharge</param>
        public string AddInvoice(DateTime invoiceDate, decimal invoiceTotal)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCharge) " +
                              "VALUES( CDate('" + invoiceDate + "'), " + invoiceTotal + ");";
                return sSQL;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }          
        }

        /// <summary>
        /// Update Invoice with new information
        /// </summary>
        /// <param name="invoiceNum">Invoice Number to update</param>
        /// <param name="invoiceDate">New Date for Invoice</param>
        /// <param name="invoiceTotal">New TotalCharge for Invoice</param>
        /// <returns></returns>
        public string UpdateInvoice(string invoiceNum, DateTime invoiceDate, decimal invoiceTotal)
        {
            try
            {
                string sSQL = "UPDATE Invoices " +
                              "SET InvoiceDate = CDate('" + invoiceDate + "'), TotalCharge = " + invoiceTotal + " " +
                              "WHERE InvoiceNum = " + invoiceNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Update a Line Item with a new item code **NOT USED CURRENTLY
        /// </summary>
        /// <param name="invoiceNum">Invoice number for selected invoice</param>
        /// <param name="lineItemNum">Line Item to update</param>
        /// <param name="itemCode">New Item code</param>
        /// <returns></returns>
        public string UpdateLineItem(string invoiceNum, string lineItemNum, string itemCode)
        {
            try
            {
                string sSQL = "UPDATE LineItems " +
                             "SET ItemCode = '" + itemCode + "' " +
                             "WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum = " + lineItemNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add a Line Item to an Invoice
        /// </summary>
        /// <param name="invoiceNum">Selected Invoice Number</param>
        /// <param name="itemCode">Item Code</param>
        /// <param name="lineItemNum">Current Line Item Number</param>
        public string AddLineItem(string invoiceNum, string lineItemNum, string itemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) " +
                              "VALUES(" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "');";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing invoice from the database
        /// </summary>
        /// <param name="invoiceNum">Invoice to delete</param>
        public string DeleteInvoice(string invoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM Invoices " +
                              "WHERE InvoiceNum = " + invoiceNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }           
        }

        /// <summary>
        /// Delete an existing Line Item from the database **NOT USED CURRENTLY
        /// </summary>
        /// <param name="invoiceNum">Selected Invoice Number</param>
        /// <param name="lineItemNum">Line Item number for item to delete</param>
        public string DeleteLineItem(string invoiceNum, string lineItemNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems " +
                              "WHERE InvoiceNum = " + invoiceNum + " " +
                              "AND LineItemNum = " + lineItemNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete all Line Items based on an Invoice Number
        /// </summary>
        /// <param name="invoiceNum">Selected Invoice Number</param>
        public string DeleteInvoiceLineItems(string invoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems " +
                              "WHERE InvoiceNum = " + invoiceNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Queries Invoices for the newest invoice
        /// </summary>
        /// <returns>Returns Invoice Number for the newest invoice</returns>
        public string GetNewestInvoice()
        {
            string sSQL = "SELECT Max(InvoiceNum) " +
                          "FROM Invoices;";
            return sSQL;
        }

        /// <summary>
        /// SQL statement to get all Items in the database 
        /// </summary>
        /// <returns>All Items in database ordered by Item Description</returns>
        public string SelectAllItems()
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc " +
                               "ORDER BY ItemDesc;";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion


        #region Search Window SQL

        /// <summary>
        /// Generates sql string to return all invoice numbers
        /// </summary>
        /// <param name="sBegin">Begin Date of Range (Default value is 4/25/2004)</param>
        /// <param name="sEnd">End Date of Range (Default value is 4/8/2016)</param>
        /// <returns>sql query string for getting invoice numbers</returns>
        public string GetInvoiceNumbersByDateRange(string sBegin, string sEnd)
        {
            try {
                return "SELECT InvoiceNum FROM INVOICES WHERE InvoiceDate >= CDate('" + sBegin + "') AND InvoiceDate <= CDate('" + sEnd + "');";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate sql query string for getting invoice information by invoiceNum
        /// </summary>
        /// <param name="sInvoiceNum">Invoice Number</param>
        /// <returns>sql query string</returns>
        public string GetInvoice(string sInvoiceNum)
        {
            try
            {
                return "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM INVOICES WHERE InvoiceNum = " + sInvoiceNum + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate sql query string for getting contents of invoice
        /// </summary>
        /// <param name="sInvoiceNum">Invoice Number</param>
        /// <returns>sql query string</returns>
        public string GetInvoiceContents(string sInvoiceNum)
        {
            try
            {
                return "SELECT ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode WHERE LineItems.InvoiceNum = " + sInvoiceNum +"; ";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate sql query string to grab all item descriptions
        /// </summary>
        /// <returns>sql query string</returns>
        public string GetItems()
        {
            try
            {
                return "SELECT ItemDesc FROM ItemDesc ORDER BY ItemCode;";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate sql query code to grab item information by item description
        /// </summary>
        /// <param name="sItemDesc">Item description</param>
        /// <returns>sql query string</returns>
        public string GetItemInformation(string sItemDesc)
        {
            try
            {
                return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemDesc = '" + sItemDesc + "';";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generate sql query string to get invoices that contain current item
        /// </summary>
        /// <param name="sItemCode">Item code</param>
        /// <returns>sql query string</returns>
        public string GetInvoicesContainingItem(string sItemDesc)
        {
            try
            {
                return "SELECT LineItems.InvoiceNum FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode WHERE ItemDesc.ItemDesc = '" + sItemDesc + "'; ";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion


        #region Edit Window SQL
        /// <summary>
        /// Add a new item to ItemDesc table
        /// </summary>
        /// <param name="sItemCode">Item Code</param>
        /// <param name="sItemDesc">Item Description</param>
        /// <param name="Cost">Item Cost</param>
        /// <returns>sql query string</returns>
        public string AddNewItem(string sItemCode, string sItemDesc, string Cost)
        {
            try
            {
                return "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) " +
                              "VALUES(" + sItemCode + ", " + sItemDesc + ", " + Cost + ");";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Delete an item from ItemDesc table
        /// </summary>
        /// <param name="sItemCode">Item Code</param>
        /// <returns>sql query string</returns>
        public string DeleteItem(string sItemCode)
        {
            try
            {
                return "DELETE FROM ItemDesc WHERE ItemCode = " + sItemCode + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Update an item in the ItemDesc table
        /// </summary>
        /// <param name="sOldItemCode">Original item code before update</param>
        /// <param name="sNewItemCode">New item code</param>
        /// <param name="sNewItemDesc">New item description</param>
        /// <param name="NewCost">New item cost</param>
        /// <returns>sql query string</returns>
        public string UpdateItem(string sOldItemCode, string sNewItemCode, string sNewItemDesc, string NewCost)
        {
            try
            {
                return "UPDATE ItemDesc SET ItemCode = " + sNewItemCode + ", ItemDesc = " + sNewItemDesc + ", Cost = " 
                    + NewCost + " WHERE ItemCode = " + sOldItemCode + ";";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
