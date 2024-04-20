using GroupProject.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        private readonly ApplicationController _controller;
        private readonly clsItemsLogic _itemsLogic = new clsItemsLogic();
        private bool _changesMade = false;

        wndAddEditItem wndAddEditItem;

        /// <summary>
        /// Constructor for wndItems class
        /// </summary>
        /// <param name="controller"></param>
        public wndItems(ApplicationController controller)
        {
            try
            {
                _controller = controller;
                InitializeComponent();
                PopulateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Populates the DataGrid with items from the database.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void PopulateDataGrid()
        {
            try
            {
                dgItems.ItemsSource = _itemsLogic.GetAllItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles selection change in DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem != null)
                {
                    var selectedItem = (clsItem)dgItems.SelectedItem;
                    txtCode.Text = selectedItem.Code;
                    txtCost.Text = selectedItem.Cost.ToString();
                    txtDescription.Text = selectedItem.Description;
                }
                else
                {
                    txtCode.Text = "";
                    txtCost.Text = "";
                    txtDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles add item button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddEditItem = new wndAddEditItem(false);
                wndAddEditItem.ShowDialog();
                if (wndAddEditItem.SaveClicked == true) // Show the window modally
                {
                    _changesMade = true;
                    // Use the entered input
                    _itemsLogic.InsertItem(wndAddEditItem.ItemCode, wndAddEditItem.ItemDescription, wndAddEditItem.ItemCost);
                    PopulateDataGrid();
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
        /// Handles edit item button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem != null)
                {
                    var selectedItem = (clsItem)dgItems.SelectedItem;

                    // Pass the selected item's details to the wndAddEditItem window
                    wndAddEditItem = new wndAddEditItem(true, selectedItem.Code, selectedItem.Description, selectedItem.Cost);
                    wndAddEditItem.ShowDialog();

                    if (wndAddEditItem.SaveClicked == true) // Show the window modally
                    {
                        _controller.ChangesMadeToItemList = true;
                        // Use the entered input
                        _itemsLogic.UpdateItem(wndAddEditItem.ItemCode, wndAddEditItem.ItemDescription, wndAddEditItem.ItemCost);
                        PopulateDataGrid();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles delete item button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem != null)
                {
                    var selectedItem = (clsItem)dgItems.SelectedItem;
                    if (_itemsLogic.IsItemOnInvoice(selectedItem.Code))
                    {
                        // Get the list of invoices the item is used on
                        List<string> invoices = _itemsLogic.GetInvoicesForItem(selectedItem.Code);
                        // Create a message that lists the invoices
                        string message = $"Cannot delete item because it is used in the following invoices:\n{string.Join(", ", invoices)}";
                        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        // Ask for confirmation
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            // Delete the item
                            _itemsLogic.DeleteItem(selectedItem.Code);
                            MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            PopulateDataGrid();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
