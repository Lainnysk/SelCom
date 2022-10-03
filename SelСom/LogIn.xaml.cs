using System;
using System.Windows;
using SelСom.DataSet_SelComTableAdapters;

namespace SelСom
{
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (new EmployeesTableAdapter().LogInQuery(Login.Text, Password.Password) != null)
            {
                new MainMenu(Convert.ToInt32(new EmployeesTableAdapter().LogInQuery(Login.Text, Password.Password))).Show();
                this.Close();
            }
            else MessageBox.Show("Неверный логин или пароль");
        }
    }
}