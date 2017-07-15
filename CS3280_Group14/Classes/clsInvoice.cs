using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
