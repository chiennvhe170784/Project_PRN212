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
    /// Interaction logic for _2_Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        DataPrnquizContext context = new DataPrnquizContext();

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            Create_Account();

        }

        private void Create_Account()
        {

            string user = tbxUserR.Text;
            string pass = tbxPassR.Password;
            string repass = tbxRePassR.Password;

            var us = context.Users.FirstOrDefault(u => u.Username == user);
            if (us == null)
            {
                if (pass.Equals(repass))
                {

                    User u = new User()
                    {
                        Username = user,
                        Password = pass,
                        Role = 0
                    };
                    context.Users.Add(u);
                    if (context.SaveChanges() > 0)
                    {
                        //file save là tiêu đề
                        MessageBoxResult m = MessageBox.Show("Register successfully! Login system now?", "Register", MessageBoxButton.OKCancel);
                        if (m == MessageBoxResult.OK)
                        {
                            Login l = new Login();
                            l.Show();
                            this.Close();
                        }
                        else if (m == MessageBoxResult.Cancel)
                        {

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Password and password confirm must be similar! ");
                }
            }
            else
            {
                MessageBox.Show("Username already exist!");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
        }
    }
}
