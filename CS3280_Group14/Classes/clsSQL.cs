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
                              "VALUES(" + invoiceDate + ", " + invoiceTotal + ");";
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
                              "SET InvoiceDate = " + invoiceDate + ", TotalCharge = " + invoiceTotal + " " +
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
        /// Update a Line Item with a new item code
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
                             "SET ItemCode = " + itemCode + " " +
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
                              "VALUES(" + invoiceNum + ", " + lineItemNum + ", " + itemCode + ");";
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
        /// Delete an existing Line Item from the database
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
        /// SQL statement to get all Items in the database 
        /// </summary>
        /// <returns></returns>
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

        #endregion


        #region Edit Window SQL

        #endregion
    }
}
