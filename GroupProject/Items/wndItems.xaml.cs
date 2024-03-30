using GroupProject.Controller;
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

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        ApplicationController _controller;
        public wndItems(ApplicationController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void cmdAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // the user should be able to enter information using text boxes
                #region Method Code

                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        private void cmdEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // should be able to click data grid, populate and enable save button
                #region Method Code

                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        private void cmdSaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // save item button
                #region Method Code

                #endregion
            }
            #region Top Level Catch Block
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion
        }

        private void cmdDeleteItem_Click(object sender, RoutedEventArgs e)
        {
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

        }

        //bool bHasItemsBeenChanged; // Set to true when an item has been added/edited/deleted. Used by main window to know if needs to refresh items list
        //bool HasItemsBeenChanged;  // Public Property

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
