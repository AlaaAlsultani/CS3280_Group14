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
        private wndSearch search;

        /// <summary>
        /// Edit Window
        /// </summary>
        private wndEdit edit;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;  //shut down application only when main window is closed

            //Initialize windows
            search = new wndSearch();
            edit = new wndEdit();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hide();//Hide this window
                search.ShowDialog();//display search window
                Show();//show this window
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Hide();
                edit.ShowDialog();
                Show();
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
    }
}
