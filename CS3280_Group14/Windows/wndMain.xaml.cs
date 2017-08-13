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
        #region Attributes
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
        #endregion

        #region Constructors
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

                //Initialize Main Window objects and attributes
                queries = new clsDBQueries();
                currentInvoice = new clsInvoice();
                selectedItems = new List<clsItem>();
                currentTotal = 0;
                currentInvoice = null;

                //Bind ComboBox to Items in database and datagrid to selectedItems
                cmbBoxItems.ItemsSource = queries.GetItems();
                dtgrdInvoiceItems.ItemsSource = selectedItems;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region Methods

        #region Button Methods
        /// <summary>
        /// Open Search Window Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Reset UI
                ResetInvoiceData();
                grpboxInvoice.IsEnabled = false;
                btnPanel.IsEnabled = true;

                Hide();//Hide this window
                searchWindow.ShowDialog();//display search window
                Show();//show this window

                //Get Select Invoice from searchWindow and Populate the data in the current window's form
                currentInvoice = clsDBQueries.CurrentInvoice;

                //Update UI
                lblInvoiceNum.Content = (currentInvoice != null) ? $"{currentInvoice.InvoiceNumber}" : "None Selected";
                ResetInvoiceData();

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
                //Reset UI
                ResetInvoiceData();
                grpboxInvoice.IsEnabled = false;
                btnPanel.IsEnabled = true;

                //Open Window
                Hide();
                editWindow.ShowDialog();
                Show();

                //Repopulate Item Combobox 
                cmbBoxItems.ItemsSource = queries.GetItems();

                //Update Items on Currently Displayed Invoice
                if (currentInvoice != null)
                {
                    currentInvoice.Items = queries.GetInvoiceContents(currentInvoice.InvoiceNumber);
                }

                ResetInvoiceData();
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
                    queries.DeleteInvoice(currentInvoice.InvoiceNumber);
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
                    //Grab currently selected item
                    clsItem selected = (clsItem)dtgrdInvoiceItems.SelectedItem;

                    //Delete Line Item
                    selectedItems.Remove(selected);
                    dtgrdInvoiceItems.Items.Refresh();
                    dtgrdInvoiceItems.SelectedItem = null;

                    //Update the total cost
                    currentTotal -= selected.Cost;
                    lblInvoiceTotal.Content = $"{currentTotal:C}";
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
                        //Add invoice to database and get New Invoice Number
                        currentInvoice = new clsInvoice("New", dpInvoiceDate.SelectedDate.Value.Date, selectedItems, currentTotal);
                        currentInvoice.InvoiceNumber = queries.AddInvoice(currentInvoice);
                        
                        //Update UI                       
                        lblInvoiceNum.Content = currentInvoice.InvoiceNumber;
                        btnDeleteInvoice.IsEnabled = true;
                        btnEditInvoice.IsEnabled = true;
                    }
                    //If updating an existing invoice
                    else
                    {
                        //Update currentInvoice Information
                        currentInvoice.InvoiceTotal = currentTotal;
                        currentInvoice.InvoiceDate = dpInvoiceDate.SelectedDate.Value.Date;
                        currentInvoice.Items.Clear();
                        foreach(var item in selectedItems)
                        {
                            currentInvoice.Items.Add(item);
                        }

                        //Update Invoice in database
                        queries.UpdateInvoice(currentInvoice);
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
        #endregion

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
                    btnDeleteInvoice.IsEnabled = false;
                    btnEditInvoice.IsEnabled = false;
                }
                //Logic for reseting to current invoice
                else
                {
                    //Reset fields back to initial state
                    dpInvoiceDate.SelectedDate = currentInvoice.InvoiceDate;
                    selectedItems.Clear();
                    currentTotal = currentInvoice.InvoiceTotal;
                    btnDeleteInvoice.IsEnabled = true;
                    btnEditInvoice.IsEnabled = true;

                    foreach (var item in currentInvoice.Items)
                    {
                        selectedItems.Add(item);
                        
                    }
                }

                //Resets Invoice Changes and disable UI
                cmbBoxItems.SelectedIndex = -1;
                dtgrdInvoiceItems.SelectedIndex = -1;
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
        /// Enables or disables the add item button on combo box change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbBoxItems.SelectedItem != null)
                {
                    btnAddItem.IsEnabled = true;
                }
                else
                {
                    btnAddItem.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Enables or disables the delete item button on datagrid selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgrdInvoiceItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtgrdInvoiceItems.SelectedItem != null)
                {
                    btnDeleteItem.IsEnabled = true;
                }
                else
                {
                    btnDeleteItem.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method to display proper line item number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgrdInvoiceItems_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.Header = (e.Row.GetIndex() + 1).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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

        #endregion
    }
}
