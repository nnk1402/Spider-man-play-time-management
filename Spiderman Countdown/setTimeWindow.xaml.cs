using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spiderman_Countdown
{
    /// <summary>
    /// Interaction logic for setTimeWindow.xaml
    /// </summary>
    public partial class setTimeWindow : Window
    {

        public int selectedMinutes { get; set; }
        public setTimeWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtMinutes.Text, out int minutes) && minutes > 0) {
                selectedMinutes = minutes;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Are you fucking kidding me ?");
            }
        }
    }
}
