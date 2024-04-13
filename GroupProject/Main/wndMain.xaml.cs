using GroupProject.Controller;
using GroupProject.DataObjects;
using GroupProject.Enum;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Search;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Cryptography;
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

        /// <summary>
        /// Holds logic for this window
        /// </summary>
        private clsMainLogic _logic;
        /// <summary>
        /// Holds logic that gets passed between windows
        /// </summary>
        private ApplicationController _controller;

        private MainViewModel _mainViewModel;


        public MainWindow()
        {
            InitializeComponent();
            _logic = new clsMainLogic();
            _controller = new ApplicationController();
            
            _mainViewModel = new MainViewModel();
            _mainViewModel.Items = _logic.getItemList();
            _mainViewModel.Data = new List<DataDisplayItem>();
            
            this.DataContext = _mainViewModel;
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

                // after this dialog is closed, will just call the method that will populate the items box
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
                if(_logic.AppState == ApplicationState.CreatingNewInvoice)
                {
                    if (checkIfUnsaved())
                    {
                        clearCurrentInvoice();
                    };                    
                } else if (_logic.AppState != ApplicationState.Default)
                {
                    throw new Exception("We are in a state that shouldn't allow this");
                }

                // create a new invoice object
                _logic.AppState = ApplicationState.CreatingNewInvoice;
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

        private bool checkIfUnsaved()
        {
            // todo : create dialog asking user if they want to clear any unsaved changes
            return true;
            throw new NotImplementedException();
        }

        private void clearCurrentInvoice()
        {
            
            _mainViewModel.Data.Clear();
            dgrdInvoiceItems.Items.Refresh();
        }

        private void buildCboItemSelection()
        {
            try
            {
                List<Item> items = _logic.getItemList();
                cboItemSelection.ItemsSource = items;
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        private void cboItemSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                Item currentItem = cboItemSelection.SelectedItem as Item;
                if(currentItem != null)
                {
                    //if an item is selected, enable the add to invoice button
                    btnAddLineItem.IsEnabled = true;
                    tbxItemcost.Text = $"${currentItem.Cost.ToString()}";
                } else
                {
                    // reset our buttons
                    iudItemQuantity.Value = 1;
                    btnAddLineItem.IsEnabled = false;
                }
                
            }
            #region Default Catch Block
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name +
                    "." +
                    MethodInfo.GetCurrentMethod().Name +
                    " -> " +
                    ex.Message);
            }
            #endregion
        }

        

        private void btnAddLineItem_Click(object sender, RoutedEventArgs e)
        {
            Item currentItem = (Item) cboItemSelection.SelectedItem;

            DataDisplayItem displayItem = new DataDisplayItem()
            {
                ItemName = currentItem.ItemDesc,
                Quantity = iudItemQuantity.Value,
                ItemCost = currentItem.Cost,
                TotalCost = (decimal) iudItemQuantity.Value * currentItem.Cost,
            };
            _mainViewModel.Data.Add(displayItem);
            _mainViewModel.TotalCost = _mainViewModel.Data.Sum(x => x.TotalCost);

            cboItemSelection.Text = "";
            
            dgrdInvoiceItems.Items.Refresh();
            lblTotal.Content = _mainViewModel.TotalCost;
            
        }

        private void btnSubmitInvoice_Click(object sender, RoutedEventArgs e)
        {
            if(dpDateSelection.SelectedDate == null)
            {
                lblSubmitError.Content = "Please select a date.";
                lblSubmitError.Visibility = Visibility.Visible;
                return;
            }

            if (_mainViewModel.Data.Count == 0)
            {
                lblSubmitError.Content = "No items to add to invoice.";
                lblSubmitError.Visibility = Visibility.Visible;
                return;
            }

            int invoiceNum = _logic.createNewInvoice(dpDateSelection.SelectedDate, _mainViewModel.TotalCost);

            tbxInvoiceNumber.Text = invoiceNum.ToString();

            lblSubmitError.Visibility = Visibility.Collapsed;
            _logic.AppState = ApplicationState.Default;
        }
    }
}