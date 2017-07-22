using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// Override ToString Method to display item description and cost
        /// </summary>
        /// <returns>Overriden string</returns>
        public override string ToString()
        {
            try
            {
                return $"{Description}: {Cost:C}";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
