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
        private clsMainLogic _logic;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSearchWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Method Code
                wndSearch wndSearch = new wndSearch();

                wndSearch.ShowDialog();
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


        /* copy paste
         
        try
            {
                #region Method Code

                #endregion
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


        try
            {
                #region Method Code

                #endregion
            }
        #region Top Level Catch Block
        catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        #endregion

         */
    }
}