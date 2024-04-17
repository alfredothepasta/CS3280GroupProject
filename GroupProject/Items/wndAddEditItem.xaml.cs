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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndAddEditItem.xaml
    /// </summary>
    public partial class wndAddEditItem : Window
    {
        /// <summary>
        /// Edit mode
        /// </summary>
        private readonly bool _isEditMode;

        /// <summary>
        /// Gets the code of the item.
        /// </summary>
        public string ItemCode { get; private set; }

        /// <summary>
        /// Gets the description of the item.
        /// </summary>
        public string ItemDescription { get; private set; }

        /// <summary>
        /// Gets the cost of the item.
        /// </summary>
        public decimal ItemCost { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the "Save" button was clicked.
        /// </summary>
        public bool SaveClicked { get; private set; }

        /// <summary>
        /// Constructor for wndAddEditItem class.
        /// </summary>
        /// <param name="isEditMode"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <param name="cost"></param>
        /// <exception cref="Exception"></exception>
        public wndAddEditItem(bool isEditMode, string code = "", string description = "", decimal cost = 0)
        {
            try
            {
                InitializeComponent();
                _isEditMode = isEditMode;

                if (_isEditMode)
                {
                    lblAddEditItem.Content = "Edit Item";
                    // Disable editing the code in edit mode
                    txtCode.IsEnabled = false;
                    // Populate the text boxes with the existing item information
                    txtCode.Text = code;
                    txtDescription.Text = description;
                    txtCost.Text = cost.ToString();
                }
                else
                {
                    lblAddEditItem.Content = "Add Item";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Event handler for the "Save" button click.
        /// </summary>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateUserInput();
                // Get the entered or edited information
                ItemCode = txtCode.Text;
                ItemDescription = txtDescription.Text;
                ItemCost = decimal.Parse(txtCost.Text);
                // Set DialogResult to true to indicate successful completion
                this.DialogResult = true;
                // Set the property to true indicating that the user clicked "Save"
                SaveClicked = true;
                // Close the window
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Event handler for the "Cancel" button click.
        /// </summary>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close(); // Close the window if cancel button is clicked
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validates user input for item code, description, and cost.
        /// </summary>
        private void ValidateUserInput()
        {
            const int maxCodeLength = 10;
            const int maxDescriptionLength = 50;
            const int maxCostLength = 20; 

            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                throw new Exception("Code and description cannot be empty.");
            }
            if (txtCode.Text.Length > maxCodeLength)
            {
                throw new Exception($"Code cannot exceed {maxCodeLength} characters.");
            }
            if (txtDescription.Text.Length > maxDescriptionLength)
            {
                throw new Exception($"Description cannot exceed {maxDescriptionLength} characters.");
            }
            if (txtCost.Text.Length > maxCostLength)
            {
                throw new Exception($"Cost value is too long.");
            }
            if (!decimal.TryParse(txtCost.Text, out decimal cost) || cost < 0)
            {
                throw new Exception("Invalid cost value.");
            }
        }

    }
}
