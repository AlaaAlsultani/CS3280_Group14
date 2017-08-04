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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS3280_Group14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Search Window
        /// </summary>
        private wndSearch searchWindow;
        /// <summary>
        /// Edit Window
        /// </summary>
        private wndEdit editWindow;
        /// <summary>
        /// Object for running queries on database
        /// </summary>
        private clsDBQueries queries;
        /// <summary>
        /// Object for holding current invoice information
        /// </summary>
        private clsInvoice currentInvoice;
        /// <summary>
        /// Object for holding item changes to invoice
        /// </summary>
        private List<clsItem> selectedItems;
        /// <summary>
        /// Holds current invoice total
        /// </summary>
        private decimal currentTotal;

        /// <summary>
        /// Main Window Constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;  //shut down application only when main window is closed

                //Initialize windows
                searchWindow = new wndSearch();
                editWindow = new wndEdit();

                //Initialize query object
                queries = new clsDBQueries();
                currentInvoice = new clsInvoice();
                selectedItems = new List<clsItem>();
                currentTotal = 0;
                currentInvoice = null;

                //Bind ComboBox to Items in database
                cmbBoxItems.ItemsSource = queries.GetItems();
                dtgrdInvoiceItems.ItemsSource = selectedItems;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Open Search Window Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hide();//Hide this window
                searchWindow.ShowDialog();//display search window
                Show();//show this window

                //Get Select Invoice from searchWindow and Populate the data in the current window's form
                //currentInvoice = queries.GetInvoiceByNumber(searchWindow.Invoice);

                //Update UI

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Open Edit Window Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hide();
                editWindow.ShowDialog();
                Show();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles New Invoice Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Enable UI
                btnPanel.IsEnabled = false;
                grpboxInvoice.IsEnabled = true;

                //Clear current invoice for edits
                currentInvoice = null;
                ResetInvoiceData();
                lblInvoiceNum.Content = "TBD";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles Edit Invoice Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if Invoice is selected and then unlock the UI for edits
                if (currentInvoice == null)
                {
                    MessageBox.Show("No Invoice is currently selected", "Edit Invoice Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    grpboxInvoice.IsEnabled = true;
                    btnPanel.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        /// <summary>
        /// Handles Delete Invoice Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if an invoice is selected
                if(currentInvoice == null)
                {
                    MessageBox.Show("No Invoice is currently selected", "Delete Invoice Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //Delete Invoice
                    //queries.DeleteInvoice(currentInvoice.InvoiceNumber);
                    //Reset UI
                    currentInvoice = null;
                    ResetInvoiceData();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles Add Item Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if an Item is selected
                if(cmbBoxItems.SelectedItem != null)
                {
                    //Grab currently selected item
                    clsItem selected = (clsItem)cmbBoxItems.SelectedItem;

                    //Adds the Currently selected item to the data grid
                    selectedItems.Add(selected);
                    dtgrdInvoiceItems.Items.Refresh();

                    //Update the total cost
                    currentTotal += selected.Cost;
                    lblInvoiceTotal.Content = $"{currentTotal:C}";
                    //dtgrdInvoiceItems.ItemsSource = selectedItems;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles Delete Line Item Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLineItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if an item is selected
                if (dtgrdInvoiceItems.SelectedItem != null)
                {
                    //Delete Line Item
                    selectedItems.Remove((clsItem)dtgrdInvoiceItems.SelectedItem);
                    //dtgrdInvoiceItems.Items.Remove(dtgrdInvoiceItems.SelectedItem);
                    dtgrdInvoiceItems.Items.Refresh();

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles Save Changes event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check for valid inputs
                if(dpInvoiceDate.SelectedDate == null || selectedItems.Count < 1)
                {
                    MessageBox.Show("Invalid Date or No Line Items Selected", "Save Invoice Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //If adding a new invoice
                    if (currentInvoice == null)
                    {
                        //Recover Invoice ID
                        string invoiceNum = "New";
                        //queries.AddInvoice();
                        //invoiceNum = queries.GetNewestInvoice();
                        currentInvoice = new clsInvoice(invoiceNum, dpInvoiceDate.SelectedDate.Value.Date, selectedItems, currentTotal);
                        lblInvoiceNum.Content = currentInvoice.InvoiceNumber;
                    }
                    //If updating an invoice
                    else
                    {
                        //queries.UpdateInvoice(currentInvoice.InvoiceNumber);
                        //???Delete Line Items????
                        //currentInvoice = queries.GetInvoiceByNumber(currentInvoice.InvoiceNumber);

                        //Temp Test Code
                        currentInvoice.InvoiceTotal = currentTotal;
                        currentInvoice.InvoiceDate = dpInvoiceDate.SelectedDate.Value.Date;
                        currentInvoice.Items.Clear();
                        foreach(var item in selectedItems)
                        {
                            currentInvoice.Items.Add(item);
                        }
                    }

                    //disable UI
                    cmbBoxItems.SelectedIndex = -1;
                    grpboxInvoice.IsEnabled = false;
                    btnPanel.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handles Cancel Changes event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetInvoiceData();
                grpboxInvoice.IsEnabled = false;
                btnPanel.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #region Helper Methods
        /// <summary>
        /// Handles Reseting Invoice Information 
        /// Based on current invoice
        /// </summary>
        private void ResetInvoiceData()
        {
            try
            {
                //Logic for null invoice
                if (currentInvoice == null)
                {
                    //Clear all fields back to empty
                    lblInvoiceNum.Content = "None Selected";
                    dpInvoiceDate.SelectedDate = null;
                    selectedItems.Clear();
                    currentTotal = 0;
                }
                //Logic for reseting to current invoice
                else
                {
                    //Reset fields back to initial state
                    dpInvoiceDate.SelectedDate = currentInvoice.InvoiceDate;
                    selectedItems.Clear();
                    currentTotal = currentInvoice.InvoiceTotal;

                    foreach (var item in currentInvoice.Items)
                    {
                        selectedItems.Add(item);
                        
                    }
                }

                //Resets Invoice Changes and disable UI
                cmbBoxItems.SelectedIndex = -1;
                lblInvoiceTotal.Content = $"{currentTotal:C}";
                dtgrdInvoiceItems.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to display proper line item number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgrdInvoiceItems_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
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
    }
}
