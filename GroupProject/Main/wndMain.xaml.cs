using GroupProject.Controller;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Search;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupProject
{
    
    
    
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /********How I envision this working*************
         * 
         ************************************************/

        /// <summary>
        /// Holds logic for this window
        /// </summary>
        private clsMainLogic _logic;
        /// <summary>
        /// Holds logic that gets passed between windows
        /// </summary>
        private ApplicationController _controller;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateDataGrid()
        {
            // this should be called after the search dialogue is closed
            // it will get the invoice and then populate the data grid
        }

        private void OpenSearchWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                // there will be code to check if this is an acceptable option

                wndSearch wndSearch = new wndSearch();

                wndSearch.ShowDialog();

                // after the dialogue is closed, pull the new invoice id from the app controller
                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        private void OpenEditWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                wndItems itemsWindow = new wndItems(_controller);

                itemsWindow.ShowDialog();
                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        private void btnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                // create a new invoice object

                grdNewInvoice.Visibility = Visibility.Visible;


                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }
    }
}