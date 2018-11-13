using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibrarySystem
{
    class Login_Class
    {

        /* Conn is an object of the connection string which is declared outside the methods and will be used in all methods to access the sql connection*/
        SqlConnection Conn_Login = new SqlConnection("Data Source=WT135-826LSW\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");

        /* cmd is an object of the sqlcommand class. Which is declared outside the method and will be used in all methods to run sql command*/
        SqlCommand cmd_login = new SqlCommand();

        /* Reader is an object of the Reader which is declared outside the methods and will be used in all methods*/
        SqlDataReader Reader_Login;

        /* Query is a string type variable which will be used to store the sql queries (select, insert, delete or update)*/
        String Query_login;


        /* Login method is a pure method because it takes 2 strings i.e. username and password run select query on that 
         * and then return boolean value i.e. true or false based on queries results i.e. query is pass or false  */
        public bool Login_method(string username, string password)
        {
            try
            {
                cmd_login.Connection = Conn_Login;

                /* Store parameterized select queries into Query string. 
                 * Don't ever write query like select * from Login where username=  '" + username + "' and password =  '" + password + "'";*/
                Query_login = "Select username, password from Login where username =  @UserName  and password =  @Password ";

                /* add parameters to command object. And there are multiple methords to write the query using parameters
                 * 
                 * Method 1: cmd.Parameters.AddWithValue("@UserName", username); I used this menthod"
                 * 
                 * Method 2: as mentioned below
                 * SqlParameter[] param = new SqlParameter[2];  
                 * param[0] = new SqlParameter("@UserName", username);  
                 * param[1] = new SqlParameter("@Password", password);  
                 * cmd.Parameters.Add(param[0]);  
                 * cmd.Parameters.Add(param[1]); 
                 * 
                 * Method 3: as mentioned below
                 * SqlParameter[] param  = new SqlParameter[2];
			     * param[0].ParameterName = "@UserName";
			     * param[0].Value         = username;
                 * cmd.Parameters.Add(param[0]);
                 * param[1].ParameterName = "@Password";
			     * param[1].Value         = password;
                 * cmd.Parameters.Add(param[1]);
                 */
                cmd_login.Parameters.AddWithValue("@UserName", username);
                cmd_login.Parameters.AddWithValue("@password", password);

                cmd_login.CommandText = Query_login;
                //connection opened
                Conn_Login.Open();

                // get data stream
                Reader_Login = cmd_login.ExecuteReader();

                if (Reader_Login.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return false;
            }
            finally
            {
                // close reader
                if (Reader_Login != null)
                {
                    Reader_Login.Close();
                }

                // close connection
                if (Conn_Login != null)
                {
                    Conn_Login.Close();
                }
            }
        }

    }
}
