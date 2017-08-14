using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
        /// Validates that the item description is a string
        /// </summary>
        /// <param name="input">string</param>
        /// <returns></returns>
        public bool ValidItemDesc(string input)
        {
            bool result = Regex.IsMatch(input, "^[a-z,A-Z, ]+$");
            return result;
        }

        /// <summary>
        /// Validates that the item code is a string
        /// </summary>
        /// <param name="input">string</param>
        /// <returns></returns>
        public bool ValidItemCode(string input)
        {
            bool result = Regex.IsMatch(input, "^[a-z,A-Z,0-9]+$");
            return result;
        }

        /// <summary>
        /// Validates that the item cost is a decimal
        /// </summary>
        /// <param name="input">string</param>
        /// <returns></returns>
        public bool ValidItemCost(string input)
        {
            bool result = Regex.IsMatch(input, "^(\\$)[1-9][0-9]+(.[0-9][0-9]?)?$");
            return result;
        }

        /// <summary>
        /// Override ToString Method to display item description
        /// </summary>
        /// <returns>Overriden string</returns>
        public override string ToString()
        {
            try
            {
                return Description;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
