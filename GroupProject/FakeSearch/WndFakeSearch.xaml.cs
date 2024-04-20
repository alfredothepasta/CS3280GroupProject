using GroupProject.Controller;
using GroupProject.FakeSearch;
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

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for ClsFakeSearch.xaml
    /// </summary>
    public partial class WndFakeSearch : Window
    {
        /// <summary>
        /// holds the data for display
        /// </summary>
        private FakeSearchViewModel _viewModel;
        /// <summary>
        /// hold the logic object
        /// </summary>
        private ClsFakeSearchLogic _logic;
        /// <summary>
        /// passes data back to the controller
        /// </summary>
        private ApplicationController _controller;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"></param>
        /// <exception cref="Exception"></exception>
        public WndFakeSearch(ApplicationController controller)
        {
            try
            {
                _controller = controller;
                _logic = new ClsFakeSearchLogic();
                _viewModel = new FakeSearchViewModel();

                _viewModel.InvoiceNumbers = _logic.GetInvoiceNumbers();


                this.DataContext = _viewModel;

                InitializeComponent();
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
        /// handles the cancel button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        /// <summary>
        /// Handles the select button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboInvoiceList.SelectedItem != null)
                {
                    int selectedInvoiceNumber = (int)cboInvoiceList.SelectedItem;
                    _controller.SearchInvoiceNumber = selectedInvoiceNumber;
                    this.DialogResult=true;
                    this.Close();
                }
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
