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

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for _4_adminScr.xaml
    /// </summary>
    public partial class adminScr : Window
    {
        public adminScr()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateTest ct = new CreateTest();
          
            ct.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            Login l = new Login();
            l.Show();
            this.Close();
        }
    }
}
