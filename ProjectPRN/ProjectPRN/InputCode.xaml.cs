using ProjectPRN.Models;
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
    /// Interaction logic for InputCode.xaml
    /// </summary>
    public partial class InputCode : Window
    {
        private string Username;
        public InputCode(String user)
        {
            InitializeComponent();
            Username = user;
        }
        DataPrnquizContext context = new DataPrnquizContext();
        private void Button_Join_Click(object sender, RoutedEventArgs e)
        {
            string code = txtCodeE.Text;
            if(context.Tests.FirstOrDefault(x => x.Code == code) != null)
            {
                Quiz q = new Quiz(code, Username);
              
                q.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Code exam not exist!", "not exist", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
          
            History1 ht = new History1(Username);
            ht.Show();
            this.Close();
        }

        private void Button_Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login l = new Login();
            l.Show();
        }
    }
}
