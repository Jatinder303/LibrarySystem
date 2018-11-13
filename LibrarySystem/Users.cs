using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibrarySystem
{
    public class Users
    {
        /* Conn is an object of the connection string which is declared outside the methods and will be used in all methods to access the sql connection*/
        SqlConnection Conn_User = new SqlConnection("Data Source=WT135-826LSW\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");

        /* cmd is an object of the sqlcommand class. Which is declared outside the method and will be used in all methods to run sql command*/
        SqlCommand cmd_User = new SqlCommand();

        /* Reader is an object of the Reader which is declared outside the methods and will be used in all methods*/
        SqlDataReader Reader_User;

        /* Query is a string type variable which will be used to store the sql queries (select, insert, delete or update)*/
        String Query_User;



        /* UserDG method is used to fetch data from database and return the User data table to User Data Grid. 
       * So that User data will be displayed into User Data Grid*/
        public DataTable UserDG()
        {
            DataTable User_dt = new DataTable();
            try
            {
                cmd_User.Connection = Conn_User;
                Query_User = "Select UserID, FullName, Age, Username from Users";
                cmd_User.CommandText = Query_User;
                //connection opened
                Conn_User.Open();

                // get data stream
                Reader_User = cmd_User.ExecuteReader();

                if (Reader_User.HasRows)
                {
                    User_dt.Load(Reader_User);
                }
                return User_dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                // close reader
                if (Reader_User != null)
                {
                    Reader_User.Close();
                }

                // close connection
                if (Conn_User != null)
                {
                    Conn_User.Close();
                }
            }

        }



        /* Add_Users method taking 3 inputs i.e. Username, FullName and Age, which are used to pass the value to insert query
       * after that insert query inserts new Users data into database.*/
        public void Add_Users(string UserName, string FullName, int Age)
        {
            try
            {
                cmd_User.Parameters.Clear();
                cmd_User.Connection = Conn_User;

                /* Store parameterized select queries into Query string. 
                * Don't ever write query like Insert into Users(FullName, Age, UserName) Values( '"+ FullName +"', '"+ Age +"' ,'"+ UserName+"')";*/

                Query_User = "Insert into Users(FullName, Age, Username) Values(@FullName, @Age, @UserName)";

                /* add parameters to command object. */
                
                cmd_User.Parameters.AddWithValue("@UserName", UserName);
                cmd_User.Parameters.AddWithValue("@FullName", FullName);
                cmd_User.Parameters.AddWithValue("@Age", Age);

                cmd_User.CommandText = Query_User;

                //connection opened
                Conn_User.Open();

                // Executed query
                cmd_User.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_User != null)
                {
                    Conn_User.Close();
                }
            }
        }

        /* Delete_User method is taking UserID as an input which is used by delete query to delete a User from the database */
        public void Delete_Users(Int32 UserID)
        {
            try
            {
                cmd_User.Parameters.Clear();
                cmd_User.Connection = Conn_User;
                Query_User = "Delete from Users where UserID like @User_ID";

                /* add parameters to command object*/
                cmd_User.Parameters.AddWithValue("@User_ID", UserID);

                cmd_User.CommandText = Query_User;
                //connection opened

                Conn_User.Open();

                // Executed query
                cmd_User.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_User != null)
                {
                    Conn_User.Close();
                }
            }
        }

        /*Update_Users method is taking 4 inputs i.e. UserID, UserName, FullName, Age which are used in update query to update User data in database */
        public void Update_Users(int UserID, string UserName, string FullName, int Age)
        {
            try
            {
                cmd_User.Parameters.Clear();
                cmd_User.Connection = Conn_User;
                Query_User = "Update Users Set Username = @User_Name, FullName = @Full_Name, Age = @Age_User where UserID = @UserID";

                /* add parameters to command object.*/

                cmd_User.Parameters.AddWithValue("@UserID", UserID);
                cmd_User.Parameters.AddWithValue("@User_Name", UserName);
                cmd_User.Parameters.AddWithValue("@Full_Name", FullName);
                cmd_User.Parameters.AddWithValue("@Age_User", Age);

                cmd_User.CommandText = Query_User;

                //connection opened
                Conn_User.Open();

                // Executed query
                cmd_User.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_User != null)
                {
                    Conn_User.Close();
                }
            }
        }

    }
}
