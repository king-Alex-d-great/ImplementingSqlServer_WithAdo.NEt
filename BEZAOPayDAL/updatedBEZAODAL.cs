﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BEZAOPayDAL.Models;

namespace BEZAOPayDAL
{
    public partial class BEZAODAL
    {      
        public User GetUser(int id)
        {
            OpenConnection();
            User user = null;
            //string newQuery = $"Select * From Users where Id = {id}"; //witout parameterized query

            string newQuery = "Select * From Users where Id =(@Id)";

            using (IDbCommand command = new SqlCommand() { CommandText = newQuery })
            {
                SqlParameter parameter = new SqlParameter 
                { 
                    ParameterName = "@Id",
                    Value = id, 
                    SqlDbType = SqlDbType.Int                   
                };
                command.Parameters.Add(parameter);


                command.Connection = _sqlConnection;

                var datareader = command?.ExecuteReader();
                while (datareader.Read())
                {
                    for (var i = 0; i < datareader.FieldCount; i++)
                    {
                        user = new User
                        {
                            Email = (string)datareader["Email"],
                            Id = (int)datareader["Id"],
                            Name = (string)datareader["Name"],
                        };
                    }
                    // Console.WriteLine($"-> {result.GetName(i)} is a {result.GetValue(i)}");

                }
                datareader.Close();
            }
            return user;
        }

        public int InsertUser(int Id, string Name, string Email)
        {
            OpenConnection();
            int AffectedRow;
            //string newQuery = $"Insert Into Users (Id, Name, Email) Values ('{Id}', '{Name}', '{Email}')"; //witout parameterized query

            string newQuery = "Insert Into Users (Id, Name, Email) Values (@Id, @Name, @Email)";


            using (IDbCommand command = new SqlCommand(newQuery, _sqlConnection))
            {
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = Id,
                    SqlDbType = SqlDbType.Int,                    
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = Name,
                    SqlDbType = SqlDbType.Char,
                    Size = 20
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = Email,
                    SqlDbType = SqlDbType.Char,
                    Size = 20
                };
                command.Parameters.Add(parameter);

               
                AffectedRow = command.ExecuteNonQuery();
                 
            }
            CloseConnection();
          //  return AffectedRow;
            return 0;
        }

        public int InsertUserDynamically(User user)
        {
            OpenConnection();
            int AffectedRow;
            //string newQuery = $"Insert Into Users (Id, Name, Email) Values ('{user.Id}', '{user.Name}', '{user.Email}')"; //witout parameterized query

            string newQuery = "Insert Into Users (Id, Name, Email) Values (@Id, @Name, @Email)";

            using (IDbCommand command = new SqlCommand(newQuery, _sqlConnection))
            {
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = user.Id,
                    SqlDbType = SqlDbType.Int,
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name,
                    SqlDbType = SqlDbType.Char,
                    Size = 20
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = user.Email,
                    SqlDbType = SqlDbType.Char,
                    Size = 20
                };
                command.Parameters.Add(parameter);                

                AffectedRow = command.ExecuteNonQuery();
            }
            CloseConnection();
            //Console.WriteLine();
            return AffectedRow;
        }

        public int DeleteUser(User user)
        {
            OpenConnection();
            int AffectedRow;
            int Row;
            //  string newQuery = $"Delete * From Users where Id = {Id}"; //witout parameterized query
            string newQuery = "Delete * From Users where Id = (@Id)";


            using (IDbCommand command = new SqlCommand() { CommandText = newQuery })
            {
                command.Connection = _sqlConnection;

                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = user.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(parameter);                

                parameter = new SqlParameter
                {
                    ParameterName = "@Row",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(parameter);

                Row = command.ExecuteNonQuery();
                AffectedRow = (int)command.Parameters["@Row"];
            }
            
            CloseConnection();
            return AffectedRow;
        }

        public int UpdateUser(User user, string newEmail = null, string newName = null)
        {
            //A user CANNOT update an ID, just name and email!
            OpenConnection();
            int AffectedRow;

            // string Emailquery = $"Update Users Set Email = '{newEmail}' Where Id = '{id}'"; //witout parameterized query
            // string Namequery = $"Update Users Set Name = '{newName}' Where Id = '{id}'"; //witout parameterized query

            string Emailquery = "Update Users Set Email = '(@newEmail)' Where Id = '(@id)'";
            string Namequery = "Update Users Set Name = '(@newName)' Where Id = (@Id)";


            using (SqlCommand command = new SqlCommand() { Connection = _sqlConnection })
            {
                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value =user.Id,
                    SqlDbType = SqlDbType.Int,
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value =newName,
                    SqlDbType = SqlDbType.Char,
                    Size = 10
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = newEmail,
                    SqlDbType = SqlDbType.Char,
                    Size = 10
                };
                command.Parameters.Add(parameter);

               /* parameter = new SqlParameter
                {
                    ParameterName = "@AffectedRow",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(parameter);*/

                if (newEmail != null && newName == null)
                {
                    command.CommandText = Emailquery;
                }
                if (newEmail == null && newName != null)
                {
                    command.CommandText = Namequery;
                }
                if (newEmail != null && newName != null)
                {
                    command.CommandText = $"Update User Set Name = '{newName}' Where Id = '{user.Id}'; Update User Set Email = '{newEmail}' Where Id = '{user.Id}'";
                }
                else
                {
                   Console.WriteLine("you did not specify a column to update ");
                   Console.WriteLine("parameter one = Id Of User you chose to update\nparameter two = New Email\n Parameter three = New Name");
                }
                AffectedRow = command.ExecuteNonQuery();
            }
            CloseConnection();
            return AffectedRow;
        }

        public void CapturePotentialScammer (bool throwEX, User user)
        {
            OpenConnection();
            string userName;
            string SelectUserQuery = "Select * From Users where Id =(@Id)";
            string DeleteUserQuery = "DELETE From Users where Id =(@Id)";
            string InsertUserQuery = "INSERT Into Users (Id, Name, Email) Values (@Id, @Name, @Email)";

            using (var SelectUser = new SqlCommand(SelectUserQuery, _sqlConnection))
            {

                SqlParameter parameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = user.Id,
                    SqlDbType = SqlDbType.Int,
                };
                SelectUser.Parameters.Add(parameter);

                using (var dataReader = SelectUser.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        userName = (string)dataReader["Name"];
                    } else
                    {
                        CloseConnection();
                        return;
                    }
                }
            }

            var DeleteUser = new SqlCommand(DeleteUserQuery, _sqlConnection);
            var InsertUser = new SqlCommand(InsertUserQuery, _sqlConnection);

            SqlParameter Parameter = new SqlParameter
            {
                ParameterName = "@Id",
                Value = user.Id,
                SqlDbType = SqlDbType.Int,
            };
            DeleteUser.Parameters.Add(Parameter);
            InsertUser.Parameters.Add(Parameter);

            Parameter = new SqlParameter
            {
                ParameterName = "@Name",
                Value = user.Name,
                SqlDbType = SqlDbType.Char,
                Size = 20
            };
            InsertUser.Parameters.Add(Parameter);

            Parameter = new SqlParameter
            {
                ParameterName = "@Email",
                Value = user.Email,
                SqlDbType = SqlDbType.Char,
                Size = 20
            };
            InsertUser.Parameters.Add(Parameter);

            SqlTransaction transaction = null;
            try
            {
                transaction = _sqlConnection.BeginTransaction();

                //VERY IMPORTANT --> Enlist d commands into d transaction
                DeleteUser.Transaction = transaction;
                InsertUser.Transaction = transaction;

                DeleteUser.ExecuteNonQuery();
                InsertUser.ExecuteNonQuery();

                if (throwEX)
                { 
                    throw new Exception("Sorry!  Database error! Transaction failed..."); 
                }
                transaction.Commit();
            } 
            catch(Exception error)
            {
                Console.WriteLine(error.Message);   
                transaction?.Rollback();
            }
            CloseConnection();

            
        }
    }
}

