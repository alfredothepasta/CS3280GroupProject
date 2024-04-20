using GroupProject.Controller;
using GroupProject.DataObjects;
using GroupProject.Enum;
using GroupProject.Items;
using GroupProject.Main;
using GroupProject.Search;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
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

        #region Params

        /// <summary>
        /// Holds logic for this window
        /// </summary>
        private clsMainLogic _logic;
        /// <summary>
        /// Holds logic that gets passed between windows
        /// </summary>
        private ApplicationController _controller;

        /// <summary>
        /// Holds the data for display items
        /// </summary>
        private MainViewModel _mainViewModel;

        #endregion

        #region constructor
        /// <summary>
        /// Constructs the main window
        /// </summary>
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
        #endregion

        #region Combo Boxes
        /// <summary>
        /// Populates the item selection combo box
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void buildCboItemSelection()
        {
            try
            {
                _mainViewModel.Items = _logic.getItemList();
                cboItemSelection.ItemsSource = _mainViewModel.Items;
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

        /// <summary>
        /// Handles the logic for when a selection is made on the item selection combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboItemSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                Item currentItem = cboItemSelection.SelectedItem as Item;
                if (currentItem != null)
                {
                    //if an item is selected, enable the add to invoice button
                    btnAddLineItem.IsEnabled = true;
                    tbxItemcost.Text = $"${currentItem.Cost.ToString()}";
                }
                else
                {
                    // reset our buttons
                    iudItemQuantity.Value = 1;
                    btnAddLineItem.IsEnabled = false;
                }

            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }
        #endregion

        #region Button Clicks

        /// <summary>
        /// Handles the logic for when the new invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                switch (_controller.AppState)
                {
                    case ApplicationState.Default:
                        grdNewInvoice.IsEnabled = true;
                        break;
                    case ApplicationState.CreatingNewInvoice:
                        grdNewInvoice.IsEnabled = true;
                        clearCurrentInvoice();
                        break;
                    case ApplicationState.ViewingInvoice:
                        clearCurrentInvoice();
                        grdNewInvoice.IsEnabled = true;
                        break;
                    default:
                        throw new Exception("We are in a state that shouldn't allow this");
                }

                // create a new invoice object
                _controller.UpdateAppState(ApplicationState.CreatingNewInvoice);
                applicationStateChange();



                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// Handles the logic for when the add line button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLineItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_controller.AppState)
                {
                    case ApplicationState.EditingRow:
                        // todo : stuff
                        EditGridItem();
                        _controller.RevertState();
                        applicationStateChange();
                        break;
                    case ApplicationState.CreatingNewInvoice:
                    case ApplicationState.EditingInvoice:
                        AddItemToGrid();
                        break;
                    default: throw new Exception("How did we get here?");

                }
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (_controller.AppState)
                {
                    case ApplicationState.CreatingNewInvoice:
                        if (dpDateSelection.SelectedDate == null)
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

                        _controller.CurrentInvoiceId = _logic.createNewInvoice(dpDateSelection.SelectedDate, _mainViewModel.TotalCost, _mainViewModel.Data);

                        tbxInvoiceNumber.Text = _controller.CurrentInvoiceId.ToString();

                        _controller.UpdateAppState(ApplicationState.ViewingInvoice);
                        applicationStateChange();

                        lblSubmitError.Visibility = Visibility.Collapsed;
                        break;
                    case ApplicationState.EditingInvoice:
                        if (dpDateSelection.SelectedDate == null)
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

                        _logic.UpdateInvoice(_controller.CurrentInvoiceId, _mainViewModel.TotalCost, _mainViewModel.Data);

                        _controller.UpdateAppState(ApplicationState.ViewingInvoice);
                        applicationStateChange();

                        lblSubmitError.Visibility = Visibility.Collapsed;

                        break;
                    default: throw new Exception("We are in an invalid application state");
                }
                
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// Handles how the button click for the edit invoice works. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_controller.AppState == ApplicationState.ViewingInvoice)
                {
                    _controller.UpdateAppState(ApplicationState.EditingInvoice);
                    applicationStateChange();

                }
                else
                {
                    throw new Exception("We are in a state that shouldn't allow us to get here.");
                }
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// Handles how the button click for the edit row works.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int dataIndex = dgrdInvoiceItems.SelectedIndex;
                DataDisplayItem data; 
                try
                {
                    data = _mainViewModel.Data[dataIndex];
                } catch
                {
                    throw new Exception("A valid line item must be selected.");
                }

                cboItemSelection.Text = data.ItemName;
                iudItemQuantity.Value = data.Quantity;

                _controller.UpdateAppState(ApplicationState.EditingRow);
                applicationStateChange();
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

        }

        /// <summary>
        /// Handles how the button click for the delete row works.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int dataIndex = dgrdInvoiceItems.SelectedIndex;
                try
                {
                    if(_controller.AppState == ApplicationState.EditingRow)
                    {
                        throw new Exception();
                    } else
                    {
                        _mainViewModel.Data.RemoveAt(dataIndex);
                        FinalizeGridChanges();
                    }
                }
                catch
                {
                    if(_controller.AppState == ApplicationState.EditingRow)
                    {
                        throw new Exception("Please finish editing the current row before deleting a row.");
                    }
                    throw new Exception("A valid line item must be selected.");
                }
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// Handles how the button click for the cancel button works.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string messageBoxText = "Canceling will discard unsaved changes.";
                string caption = "Save Warning";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon);
                
                if (result == MessageBoxResult.OK) {
                    clearCurrentInvoice();
                }
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        #endregion

        #region Open Windows
        /// <summary>
        /// Opens the search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSearchWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                // there will be code to check if this is an acceptable option
                _controller.UpdateAppState(ApplicationState.SearchingInvoice);
                

#if true
                WndFakeSearch wndFakeSearch = new WndFakeSearch(_controller);
                wndFakeSearch.ShowDialog();
                applicationStateChange();
                editingInvoice();
#else
                // since this part is not being done, here's how I WOULD be doing it
                wndSearch wndSearch = new wndSearch();

                wndSearch.ShowDialog();
                if(wndSearch.DialogResult == true)
                {
                    // _controller.SearchInvoiceNumber was set in the search window
                    _controller.AppState = ApplicationState.EditingInvoice;
                    applicationStateChange();
                }
#endif

                if (wndFakeSearch.DialogResult == true)
                {
                    // _controller.SearchInvoiceNumber was set in the search window
                    _controller.UpdateAppState(ApplicationState.EditingInvoice);
                    applicationStateChange();
                    editingInvoice();
                }

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

        /// <summary>
        /// Opens the edit window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenEditWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                wndItems itemsWindow = new wndItems(_controller);

                itemsWindow.ShowDialog();

                if (_controller.ChangesMadeToItemList == true)
                {
                    buildCboItemSelection();

                    _logic.updateCurrentInvoiceDisplay(_mainViewModel);
                    FinalizeGridChanges();
                    _controller.ChangesMadeToItemList = false;
                }
                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        #endregion

        #region Helper Methods

        private void clearCurrentInvoice()
        {
            _controller = new ApplicationController();
            applicationStateChange();
            tbxInvoiceNumber.Text = string.Empty;
            dpDateSelection.Text = string.Empty;
            _mainViewModel.Data.Clear();
            FinalizeGridChanges();
        }

        /// <summary>
        /// Handles how the buttons should be displayed if the application state changes
        /// </summary>
        private void applicationStateChange()
        {
            try
            {
                switch (_controller.AppState)
                {
                    case ApplicationState.ViewingInvoice:
                        MenuItemEditItems.IsEnabled = true;
                        MenuItemSearch.IsEnabled = true;
                        MenuItemInvoiceEdit.IsEnabled = true;
                        MenuItemNewInvoice.IsEnabled = true;

                        grdNewInvoice.IsEnabled = false;
                        break;
                    case ApplicationState.CreatingNewInvoice:
                        MenuItemSearch.IsEnabled = false;
                        MenuItemInvoiceEdit.IsEnabled = false;
                        MenuItemNewInvoice.IsEnabled = false;

                        btnDeleteRow.IsEnabled = true;
                        btnSubmitInvoice.Content = "Submit Invoice";
                        btnAddLineItem.Content = "Add Item";
                        break;
                    case ApplicationState.EditingInvoice:
                        MenuItemSearch.IsEnabled = false;
                        MenuItemInvoiceEdit.IsEnabled = false;
                        MenuItemNewInvoice.IsEnabled = false;

                        btnDeleteRow.IsEnabled = true;
                        btnSubmitInvoice.Content = "Save Changes";
                        grdNewInvoice.IsEnabled = true;
                        btnAddLineItem.Content = "Add Item";
                        break;
                    case ApplicationState.EditingRow:
                        MenuItemSearch.IsEnabled = false;
                        MenuItemInvoiceEdit.IsEnabled = false;
                        MenuItemNewInvoice.IsEnabled = false;

                        btnDeleteRow.IsEnabled = false;
                        btnAddLineItem.Content = "Save Row";
                        break;
                    case ApplicationState.Default:
                        MenuItemSearch.IsEnabled = true;
                        MenuItemInvoiceEdit.IsEnabled = false;
                        MenuItemNewInvoice.IsEnabled = true;


                        grdNewInvoice.IsEnabled = false;

                        grdNewInvoice.IsEnabled = false;
                        break;
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

        /// <summary>
        /// Adds an item to the active invoice and displays it in the grid
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void AddItemToGrid()
        {
            try
            {
                Item currentItem = (Item)cboItemSelection.SelectedItem;



                DataDisplayItem displayItem = new DataDisplayItem()
                {
                    ItemCode = currentItem.ItemCode,
                    ItemName = currentItem.ItemDesc,
                    Quantity = iudItemQuantity.Value,
                    ItemCost = currentItem.Cost,
                    TotalCost = (decimal)iudItemQuantity.Value * currentItem.Cost,
                };
                if (_mainViewModel.Data.Contains(displayItem))
                {
                    var updateIndex = _mainViewModel.Data.IndexOf(displayItem);
                    displayItem.Quantity += _mainViewModel.Data[updateIndex].Quantity;
                    _mainViewModel.Data.Remove(displayItem);
                    _mainViewModel.Data.Insert(updateIndex, displayItem);

                }
                else
                {
                    _mainViewModel.Data.Add(displayItem);
                }

                FinalizeGridChanges();
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

        /// <summary>
        /// Sets up the ui when an invoice is being edited
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void editingInvoice()
        {
            try
            {
                if (_controller.PreviousState == ApplicationState.SearchingInvoice)
                {
                    if (_controller.SearchInvoiceNumber != 0)
                    {
                        _controller.CurrentInvoiceId = _controller.SearchInvoiceNumber;
                        List<DataDisplayItem> newData = _logic.getInvoiceItemList(_controller.CurrentInvoiceId);

                        _mainViewModel.Data.Clear();
                        _mainViewModel.Data = newData;

                        dgrdInvoiceItems.ItemsSource = _mainViewModel.Data;
                        FinalizeGridChanges();

                        tbxInvoiceNumber.Text = _controller.CurrentInvoiceId.ToString();

                        DateTime invoiceDate = _logic.GetInvoiceDate(_controller.CurrentInvoiceId);

                        dpDateSelection.SelectedDate = invoiceDate;


                    }
                    else
                    {
                        throw new Exception("The search failed to get a valid invoice number.");
                    }


                }
                else if (_controller.PreviousState == ApplicationState.CreatingNewInvoice)
                {
                    if (tbxInvoiceNumber.Text == "TBD")
                    {
                        throw new Exception("Invoice has not been created");
                    }
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

        /// <summary>
        /// Updates the grid and the ui when changes are made that affect it
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void FinalizeGridChanges()
        {
            try
            {
                _mainViewModel.TotalCost = _mainViewModel.Data.Sum(x => x.TotalCost);
                cboItemSelection.Text = "";
                dgrdInvoiceItems.ItemsSource = _mainViewModel.Data;
                dgrdInvoiceItems.Items.Refresh();
                lblTotal.Content = _mainViewModel.TotalCost;
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

        /// <summary>
        /// Updates the contents of a grid item
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void EditGridItem()
        {
            try
            {
                Item currentItem = (Item)cboItemSelection.SelectedItem;
                int dataIndex = dgrdInvoiceItems.SelectedIndex;
                DataDisplayItem gridRow = _mainViewModel.Data[dataIndex];
                gridRow.ItemCode = currentItem.ItemCode;
                gridRow.ItemName = currentItem.ItemDesc;
                gridRow.ItemCost = currentItem.Cost;
                gridRow.Quantity = iudItemQuantity.Value;
                gridRow.TotalCost = (decimal)iudItemQuantity.Value * currentItem.Cost;
                FinalizeGridChanges();
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

        #endregion
    }
}