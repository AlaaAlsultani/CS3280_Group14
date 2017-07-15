using System;
using System.Collections.Generic;
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
        /// Class Constructor
        /// </summary>
        public clsDBQueries()
        {
            try
            {
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
