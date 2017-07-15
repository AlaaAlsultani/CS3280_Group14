using System;
using System.Collections.Generic;
using System.Linq;
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
            db = new clsDataAccess();
        }
    }
}
