using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace LibrarySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Books Obj_Books = new Books();
        Users Obj_Users = new Users();
        int BookID, UserID, Age;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

         private void Books_Loaded(object sender, RoutedEventArgs e)
        {
            BookListsDataGridView.ItemsSource = Obj_Books.ListBooks().DefaultView;
        }

        private void SelectBookRow_Edit(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)BookListsDataGridView.SelectedItems[0];
            BookID = Convert.ToInt32(row["BookID"]);
            BookName_TextBox.Text = Convert.ToString(row["BookName"]);
            Author_TextBox.Text = Convert.ToString(row["Author"]);
            BookListsDataGridView.ItemsSource = Obj_Books.ListBooks().DefaultView;

        }

        private void AddBook_btn_Click(object sender, RoutedEventArgs e)
        {
            if(BookName_TextBox.Text != "" && Author_TextBox.Text != "")
            {
                Obj_Books.AddBooks(BookName_TextBox.Text, Author_TextBox.Text);
                BookListsDataGridView.ItemsSource = Obj_Books.ListBooks().DefaultView;
                BookName_TextBox.Text = "";
                Author_TextBox.Text = "";

            }
        }

         private void Deletebook_Btn_Click(object sender, RoutedEventArgs e)
         {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete the book?", "Library", MessageBoxButton.YesNo);
            if(dialogResult.ToString()=="Yes")
            {
                Obj_Books.DeleteBooks(BookID);
                MessageBox.Show("Book Deleted");
                BookListsDataGridView.ItemsSource = Obj_Books.ListBooks().DefaultView;
                BookName_TextBox.Text = "";
                Author_TextBox.Text = "";
            }

         }
              
        private void Updatebook_Btn_Click(object sender, RoutedEventArgs e)
        {
            string bookname = BookName_TextBox.Text;
            string author = Author_TextBox.Text;
            Obj_Books.UpdateBooks(BookID, bookname, author);
            MessageBox.Show("Book Updated");
            BookListsDataGridView.ItemsSource = Obj_Books.ListBooks().DefaultView;
            BookName_TextBox.Text = "";
            Author_TextBox.Text = "";
        }
  
        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataGridView.ItemsSource = Obj_Users.UserDG().DefaultView;
        }

        private void SelectUserRow_Edit(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)UserDataGridView.SelectedItems[0];
            UserID = Convert.ToInt32(row["UserID"]);
            UserName_TextBox.Text = Convert.ToString(row["Username"]);
            FullName_TextBox.Text = Convert.ToString(row["FullName"]);
            Age_TextBox.Text = Convert.ToString(row["Age"]);
            UserDataGridView.ItemsSource = Obj_Users.UserDG().DefaultView;

        }

        private void AddCustomer_btn_Click(object sender, RoutedEventArgs e)
        {
            Age = Convert.ToInt32(Age_TextBox.Text);
            if (UserName_TextBox.Text != "" && FullName_TextBox.Text != "" && Age != 0)
            {
                Obj_Users.Add_Users(UserName_TextBox.Text, FullName_TextBox.Text, Age);
                UserDataGridView.ItemsSource = Obj_Users.UserDG().DefaultView;
                UserName_TextBox.Text = "";
                FullName_TextBox.Text = "";
                Age_TextBox.Text = "";
            }
        }

        private void UpdateCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            string Username = UserName_TextBox.Text;
            string FullName = FullName_TextBox.Text;
            int U_ID = UserID;
            int User_Age = Convert.ToInt32(Age_TextBox.Text);
            Obj_Users.Update_Users(U_ID, Username, FullName, User_Age);
            MessageBox.Show("User Updated");
            UserDataGridView.ItemsSource = Obj_Users.UserDG().DefaultView;
            UserName_TextBox.Text = "";
            FullName_TextBox.Text = "";
            Age_TextBox.Text = "";
        }

        private void DeleteCustomer_Btn_Click(object sender, RoutedEventArgs e)
        {
            int Ur_ID = UserID;
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete the User?", "User", MessageBoxButton.YesNo);
            if (dialogResult.ToString() == "Yes")
            {
                Obj_Users.Delete_Users(UserID);
                MessageBox.Show("User Deleted");
                UserDataGridView.ItemsSource = Obj_Users.UserDG().DefaultView;
                UserName_TextBox.Text = "";
                FullName_TextBox.Text = "";
                Age_TextBox.Text = "";
            }
        }

        private void Exit_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
