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
    
    public class Books
    {
        /* Conn is an object of the connection string which is declared outside the methods and will be used in all methods to access the sql connection*/
        SqlConnection Conn_Books = new SqlConnection("Data Source=WT135-826LSW\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");

        /* cmd is an object of the sqlcommand class. Which is declared outside the method and will be used in all methods to run sql command*/
        SqlCommand cmd_Books = new SqlCommand();

        /* Reader is an object of the Reader which is declared outside the methods and will be used in all methods*/
        SqlDataReader Reader_Books;

        /* Query is a string type variable which will be used to store the sql queries (select, insert, delete or update)*/
        String Query_Books;

        internal object UserDG()
        {
            throw new NotImplementedException();
        }

        /* Listbooks method is used to fetch data from database and return the book data table to book Data Grid. 
         * So that Books data will be displayed into Book Data Grid*/
        public DataTable ListBooks()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd_Books.Connection = Conn_Books;
                Query_Books = "Select BookID, BookName, Author, Available from Books";

                cmd_Books.CommandText = Query_Books;
                //connection opened
                Conn_Books.Open();

                // get data stream
                Reader_Books = cmd_Books.ExecuteReader();

                if (Reader_Books.HasRows)
                {
                    dt.Load(Reader_Books);
                }
                return dt;
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
                if (Reader_Books != null)
                {
                    Reader_Books.Close();
                }

                // close connection
                if (Conn_Books != null)
                {
                    Conn_Books.Close();
                }
            }

        }

        /* AddBooks method taking 2 inputs i.e. bookname and authod, which are used to pass the value to insert query
         * after that insert query inserts new book data into database.*/
        public void AddBooks(string bookname, string author)
        {
            try
            {
                cmd_Books.Parameters.Clear();
                cmd_Books.Connection = Conn_Books;

                /* Store parameterized select queries into Query string. 
                * Don't ever write query like Insert into books(BookName, Author, Available) Values( '"+ bookname +"', '"+ author +"' ,'Yes')";*/

                Query_Books = "Insert into books(BookName, Author, Available) Values( @BookName, @Author ,'Yes')";

                /* add parameters to command object. And there are multiple methords to write the query using parameters
                 * 
                 * Method 1: cmd.Parameters.AddWithValue("@BookName", bookname); I used this menthod"
                 * 
                 * Method 2: as mentioned below
                 * SqlParameter[] param = new SqlParameter[2];  
                 * param[0] = new SqlParameter("@BookName", bookname);  
                 * param[1] = new SqlParameter("@Author", author);  
                 * cmd.Parameters.Add(param[0]);  
                 * cmd.Parameters.Add(param[1]); 
                 * 
                 * Method 3: as mentioned below
                 * SqlParameter[] param  = new SqlParameter[2];
			     * param[0].ParameterName = "@BookName";
			     * param[0].Value         = bookname;
                 * cmd.Parameters.Add(param[0]);
                 * param[1].ParameterName = "@Author";
			     * param[1].Value         = Author;
                 * cmd.Parameters.Add(param[1]);
                 */
                cmd_Books.Parameters.AddWithValue("@BookName", bookname);
                cmd_Books.Parameters.AddWithValue("@Author", author);
                                             
                cmd_Books.CommandText = Query_Books;

                //connection opened
                Conn_Books.Open();

                // Executed query
                cmd_Books.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_Books != null)
                {
                    Conn_Books.Close();
                }
            }
        }

        /* Deletebooks method is taking BookID as an input which is used by delete query to delete a book from the database */
        public void DeleteBooks(Int32 BookID)
        {
            try
            {
                cmd_Books.Parameters.Clear();
                cmd_Books.Connection = Conn_Books;
                Query_Books = "Delete from books where BookID like @BookID";

                /* add parameters to command object. And there are multiple methords to write the query using parameters
                * 
                * Method 1: cmd.Parameters.AddWithValue("@BookID", BookID); I used this menthod"
                * 
                * Method 2: as mentioned below
                * SqlParameter param = new SqlParameter;  
                * param = new SqlParameter("@BookID", BookID);  
                * cmd.Parameters.Add(param);  
                * 
                * Method 3: as mentioned below
                * SqlParameter param  = new SqlParameter;
                * param.ParameterName = "@BookID";
                * param.Value         = BookID;
                * cmd.Parameters.Add(param);
                */
                cmd_Books.Parameters.AddWithValue("@BookID", BookID);
               
                cmd_Books.CommandText = Query_Books;
                //connection opened

                Conn_Books.Open();

                // Executed query
                cmd_Books.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_Books != null)
                {
                    Conn_Books.Close();
                }
            }
        }

        /*UpdateBooks method is taking 3 inputs i.e. BookID, BookName, Author, which are used in update query to update Books data in database */
        public void UpdateBooks(int BookID, string BookName, string Author)
        {
            try
            {
                cmd_Books.Parameters.Clear();
                cmd_Books.Connection = Conn_Books;
                Query_Books = "Update books Set BookName = @Book_Name, Author = @Book_Author where BookID = @Book_ID";


                /* add parameters to command object. And there are multiple methords to write the query using parameters
                  * 
                  * Method 1: cmd.Parameters.AddWithValue("@BookName", bookname); I used this menthod"
                  * 
                  * Method 2: as mentioned below
                  * SqlParameter[] param = new SqlParameter[2];  
                  * param[0] = new SqlParameter("@BookName", bookname);  
                  * param[1] = new SqlParameter("@Author", author);  
                  * cmd.Parameters.Add(param[0]);  
                  * cmd.Parameters.Add(param[1]); 
                  * 
                  * Method 3: as mentioned below
                  * SqlParameter[] param  = new SqlParameter[2];
                  * param[0].ParameterName = "@BookName";
                  * param[0].Value         = bookname;
                  * cmd.Parameters.Add(param[0]);
                  * param[1].ParameterName = "@Author";
                  * param[1].Value         = Author;
                  * cmd.Parameters.Add(param[1]);
                  */
                cmd_Books.Parameters.AddWithValue("@Book_ID", BookID);
                cmd_Books.Parameters.AddWithValue("@Book_Name", BookName);
                cmd_Books.Parameters.AddWithValue("@Book_Author", Author);
               
                cmd_Books.CommandText = Query_Books;
            
                //connection opened
                Conn_Books.Open();

                // Executed query
                cmd_Books.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (Conn_Books != null)
                {
                    Conn_Books.Close();
                }
            }
        }

       
    }
}
