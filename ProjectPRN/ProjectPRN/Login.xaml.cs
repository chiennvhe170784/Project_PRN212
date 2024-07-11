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
    /// Interaction logic for _1_Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        DataPrnquizContext context = new DataPrnquizContext();

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            Button_Login_Click();
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
            this.Close();
        }

        private void keyDownPassL(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(tbxUserL.Text))
                {
                    MessageBox.Show("Username is blank! Please enter username");

                }
                else if (string.IsNullOrEmpty(tbxPassL.Password))
                {
                    MessageBox.Show("Password is blank! Please enter password");
                }
                else
                {
                    Button_Login_Click();
                }

            }
        }

        private void Button_Login_Click()
        {
            string user = tbxUserL.Text;
            string pass = tbxPassL.Password;
            var us = context.Users.FirstOrDefault(u => u.Username == user && u.Password == pass);
            if (us != null)
            {
                if (us.Role == 1)
                {
                    CreateTest Create = new CreateTest();
                    Create.Show();
                    this.Hide();
                }
                else
                {
                  InputCode inputCode = new InputCode(user);
                    inputCode.Show();
                   this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Username or password invalid!");
            }
        }

        private void keyDownUserL(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(tbxUserL.Text))
                {
                    MessageBox.Show("Username is blank! Please enter username");

                }
                else if (string.IsNullOrEmpty(tbxPassL.Password))
                {
                    MessageBox.Show("Password is blank! Please enter password");
                }
                else
                {
                    Button_Login_Click();
                }

            }
        }
    }
}
