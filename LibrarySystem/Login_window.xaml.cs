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

namespace LibrarySystem
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login_window: Window
    {
        Login_Class Obj_Login = new Login_Class();

        public Login_window()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(Obj_Login.Login_method(UserName_TextBox.Text, Password_TextBox.Text))
            {
                MessageBox.Show("Login Successful");
                MainWindow w = new MainWindow();
                w.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid User Name or Password");
                (new Login_window()).Show();
                this.Close();
            }
        }

    }
}
