using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_Group14
{
    /// <summary>
    /// Class Holds Information for an Invoice
    /// </summary>
    class clsInvoice
    {
        /// <summary>
        /// Public property for Invoice Number
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// Public property for Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// Public Property for List of Items on Invoice
        /// </summary>
        public List<clsItem> Items { get; set; }
        /// <summary>
        /// Public Property for Invoice Total
        /// </summary>
        public decimal InvoiceTotal { get; set; }

        //TODO: Validate Properties

        /// <summary>
        /// Class Constructor
        /// </summary>
        public clsInvoice()
        {
            try
            {
                Items = new List<clsItem>();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="invoiceNum">Invoice's Number</param>
        /// <param name="invoiceDate">Invoice's Date</param>
        /// <param name="invoiceItems">Invoice Line Items</param>
        /// <param name="invoiceTotal">Invoice's Total</param>
        public clsInvoice(string invoiceNum, DateTime invoiceDate, List<clsItem> invoiceItems, decimal invoiceTotal)
        {
            try
            {
                InvoiceNumber = invoiceNum;
                InvoiceDate = invoiceDate;
                InvoiceTotal = invoiceTotal;
                Items = new List<clsItem>();

                foreach (var item in invoiceItems)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
