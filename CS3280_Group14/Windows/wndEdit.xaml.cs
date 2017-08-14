using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace CS3280_Group14
{
    /// <summary>
    /// Interaction logic for wndEdit.xaml
    /// </summary>
    public partial class wndEdit : Window
    {
        #region Attributes
        /// <summary>
        /// cls that queries the database
        /// </summary>
        private clsDBQueries queries;

        /// <summary>
        /// list of clsItems
        /// </summary>
        private List<clsItem> listOfItems;

        /// <summary>
        /// Class Item object
        /// </summary>
        private clsItem item;

        /// <summary>
        /// Used in Update and Add methods
        /// </summary>
        private bool isUpdateItem;

        /// <summary>
        /// To access my window view
        /// </summary>
        private wndEdit editWindow;

        #endregion

        #region Constructor
        public wndEdit()
        {
            try
            {
                InitializeComponent();
                queries = new clsDBQueries();
                item = new clsItem();
                listOfItems = queries.GetAllFromItemDesc();
                isUpdateItem = false;
                dgListOfItems.ItemsSource = listOfItems;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Control how the window closes when the x is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Instructor provided Error Handling message system
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Updates this items Description and Cost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredDesc = txtbDesc.Text;
                string enteredCost = txtbCost.Text;


                if (item.ValidItemDesc(enteredDesc) && item.ValidItemCost(enteredCost))
                {
                    MessageBox.Show("Validation is complete you may now save changes.");
                    isUpdateItem = true;
                    btnsaveItemChanges.IsEnabled = true;
                }
                if (!(item.ValidItemDesc(enteredDesc) && item.ValidItemCost(enteredCost)))
                    MessageBox.Show("Please only enter letters for the Description(allows spaces) and numbers for the Cost");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the seelcted item from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //If the user tries to delete an item that is on an invoice, don't allow the user
            //to do so.
            //Give the user a warning message that tells them which invoices that item is used on.
            try
            {
                DataSet ds = new DataSet();

                
                ds = queries.GetInvoicesContainingItem(txtbCode.Text);
                //if (ds.IsInitialized)
                //Count Seems to work better
                if(ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Cannot delete this item for it is in an invoice.");
                }
                else
                {
                    queries.DeleteItem(txtbCode.Text);
                    listOfItems = queries.GetAllFromItemDesc();
                    dgListOfItems.ItemsSource = listOfItems;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Adds a new item to the list if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredDesc = txtbDesc.Text;
                string enteredCost = txtbCost.Text;
                isUpdateItem = false;

                if (item.ValidItemDesc(enteredDesc) && item.ValidItemCost(enteredCost))
                {
                    MessageBox.Show("Validation is complete you may now add new item by clicking the Save Changes button.");
                    btnsaveItemChanges.IsEnabled = true;
                }
                if(!(item.ValidItemDesc(enteredDesc) && item.ValidItemCost(enteredCost)))
                    MessageBox.Show("Please only enter letters for the Description(allows spaces) and numbers for the Cost");
            
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Saves changes to an item in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isUpdateItem)
                {
                    queries.UpdateItem(txtbCode.Text, txtbCost.Text, txtbDesc.Text);
                    listOfItems = queries.GetAllFromItemDesc();
                    dgListOfItems.ItemsSource = listOfItems;
                    btnsaveItemChanges.IsEnabled = false;
                }
                if (!(isUpdateItem))
                {
                    queries.AddNewItem(txtbCode.Text, txtbCost.Text, txtbDesc.Text);
                    listOfItems = queries.GetAllFromItemDesc();
                    dgListOfItems.ItemsSource = listOfItems;
                    btnsaveItemChanges.IsEnabled = false;
                }
                else
                    btnsaveItemChanges.IsEnabled = false;

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
