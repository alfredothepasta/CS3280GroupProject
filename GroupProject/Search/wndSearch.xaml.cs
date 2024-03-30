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
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        public wndSearch()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            //Item is selected, window closes, main menu focus. 
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //Item is not selected, search window closes, main menu focus. 
        }
    }
}
