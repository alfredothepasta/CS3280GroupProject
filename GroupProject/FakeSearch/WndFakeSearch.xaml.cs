using GroupProject.Controller;
using GroupProject.FakeSearch;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private FakeSearchViewModel _viewModel;
        private ClsFakeSearchLogic _logic;
        private ApplicationController _controller;
        public WndFakeSearch(ApplicationController controller)
        {
            _controller = controller;
            _logic = new ClsFakeSearchLogic();
            _viewModel = new FakeSearchViewModel();

            _viewModel.InvoiceNumbers = _logic.GetInvoiceNumbers();
            
            
            this.DataContext = _viewModel;
            
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if(cboInvoiceList.SelectedItem != null)
            {
                int selectedInvoiceNumber = (int)cboInvoiceList.SelectedItem;
                _controller.SearchInvoiceNumber = selectedInvoiceNumber;
                this.DialogResult=true;
                this.Close();
            }
        }
    }
}
