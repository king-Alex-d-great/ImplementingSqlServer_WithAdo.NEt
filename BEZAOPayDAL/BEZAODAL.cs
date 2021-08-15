using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BEZAOPayDAL.Models;

namespace BEZAOPayDAL
{
   public partial class BEZAODAL
   {
       private readonly string _connectionString;
       public BEZAODAL():
           this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BEZAOPay;Integrated Security=True")
       {

       }
       
       public BEZAODAL(string connectionString )
       {
           _connectionString = connectionString;
       }


       private SqlConnection _sqlConnection = null;
       private void OpenConnection()
       {
           _sqlConnection = new SqlConnection { ConnectionString = _connectionString };
           _sqlConnection.Open();
       }

       private void CloseConnection()
       {
           if (_sqlConnection?.State != ConnectionState.Closed) 
               _sqlConnection?.Close();
       }


       public IEnumerable<User> GetAllUsers()
       {
            OpenConnection();

            var users = new List<User>();

            var query = @"SELECT * FROM USERS";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                   users.Add(new User
                   {
                       Id = (int) reader["Id"],
                       Name = (string) reader["Name"],
                       Email =  (string) reader["Email"]
                   }); 
                }
                reader.Close();
            }

            return users;

       }


    }
}
