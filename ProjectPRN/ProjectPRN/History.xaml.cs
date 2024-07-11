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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History1 : Window
    {
        private string Username;
        public History1(String user)
        {
            InitializeComponent();
            Username = user;
            LoadData();
        }

        DataPrnquizContext context = new DataPrnquizContext();
        private void LoadData()
        {
            try
            {
                int id = context.Users.FirstOrDefault(x => x.Username.Equals(Username)).Uid;

                var data = context.Histories.Where(x => x.Uid == id).ToList();
                dataHistory.ItemsSource = data;
            }
            catch (Exception)
            {

                MessageBox.Show("Data not found");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputCode inputCode = new InputCode(Username);
            inputCode.Show();
            this.Hide();
        }
    }
}
