using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_Group14
{
    /// <summary>
    /// Class Holds information for a single item
    /// </summary>
    class clsItem
    {
        /// <summary>
        /// Public Property For Item Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Public Property for Item Cost
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Public Property for Item Description
        /// </summary>
        public string Description { get; set; }

        //TODO: Add Validation to Properties?
    }
}
