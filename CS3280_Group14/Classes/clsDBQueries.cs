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
    }
}
