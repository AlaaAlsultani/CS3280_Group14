using System;
using System.Collections.Generic;
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
    /// Interaction logic for wndSearchWindow.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// performs database query requests
        /// </summary>
        private clsDBQueries query;

        /// <summary>
        /// Will be combobox item source for invoice numbers
        /// </summary>
        private List<int> lstInvoiceSource;

        /// <summary>
        /// Will be  combobox item source for item Descriptions
        /// </summary>
        private List<string> lstItemSource;


        /// <summary>
        /// Search window constructor
        /// </summary>
        public wndSearch()
        {
            try 
            {
                InitializeComponent();

                query = new clsDBQueries();
                lstInvoiceSource = new List<int>();
                lstItemSource = new List<string>();

                lstInvoiceSource = query.GetInvoiceNumbersByDateRange(dpBeginDate.SelectedDate.Value.Date.ToString(), dpEndDate.SelectedDate.Value.Date.ToString());
                lstItemSource = query.GetItemsDesc();

                cmbInvoiceNums.ItemsSource = lstInvoiceSource;
                cmbItems.ItemsSource = lstItemSource;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

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

        /// <summary>
        /// Select Item from combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //TODO: Populate Item information box
                //TODO: Populate Datagrid with Invoices
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Submits user selected date range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSubmitRange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO: Populate Invoice Combobox with invoiceNums between date range
                //TODO: Refresh Combobox list source
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Select invoice number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //TODO:Populate Invoice Information box
                //TODO: Populate data grid displaying invoice contents
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (System.Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }

}
