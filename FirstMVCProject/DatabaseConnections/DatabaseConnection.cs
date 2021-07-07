using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FirstMVCProject.Models.ApplicationModels;

namespace FirstMVCProject.DatabaseConnections

{
    public class DatabaseConnection
    {
        const string CONNECTION_STRING = @"Server=tcp:cursosharp.database.windows.net,1433;Initial Catalog=csharpdb;User Id=application;Password=Aluno@2021;";

        List<User> allUserList = new List<User>();

        public void AddUser(User newUser)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string commandSql = "INSERT INTO API_USERS (FIRSTNAME, LASTNAME, DATEOFBIRTH) VALUES" +
                    "(@firstName, @lastName, @dob)";

                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@firstName", newUser.FirstName);
                command.Parameters.AddWithValue("@lastName", newUser.LastName);
                command.Parameters.AddWithValue("@dob", newUser.DateOfBirth);

                command.ExecuteNonQuery();
                command.Connection.Close();

            }
        }

        public void DeleteUser (int Id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string commandSql = "DELETE FROM API_USERS WHERE ID = @id";

                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@id", Id);

                command.ExecuteNonQuery();
                command.Connection.Close();
               
            }
        }

        public void UpdateUser (User userToUpdate)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string commandSql = "UPDATE API_USERS SET FIRSTNAME=@firstName, LASTNAME=@lastName," +
                    "DATEOFBIRTH=@dob WHERE ID=@userId";

                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@userId", userToUpdate.Id);
                command.Parameters.AddWithValue("@firstName", userToUpdate.FirstName);
                command.Parameters.AddWithValue("@lastName", userToUpdate.LastName);
                command.Parameters.AddWithValue("@dob", userToUpdate.DateOfBirth);

                command.ExecuteNonQuery();
                command.Connection.Close();

            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string commandSql = "SELECT * FROM API_USERS";

                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Connection.Open();

                SqlDataReader commandReader = command.ExecuteReader();

                while (commandReader.Read())
                {
                    User user = new User()
                    {
                        Id = Convert.ToInt32(commandReader["ID"]),
                        FirstName = commandReader["FIRSTNAME"].ToString(),
                        LastName = commandReader["LASTNAME"].ToString(),
                        DateOfBirth = Convert.ToDateTime(commandReader["DATEOFBIRTH"])
                    };

                    allUserList.Add(user);
                }

            }

            return allUserList;
        }

        public User GetUserById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string commandSql = "SELECT * FROM API_USERS WHERE ID=@id";

                SqlCommand command = new SqlCommand(commandSql, connection);
                command.Connection.Open();

                command.Parameters.AddWithValue("@id", Id);

                SqlDataReader commandReader = command.ExecuteReader();


                User user = new User();

                while(commandReader.Read())
                {
                    user.Id = Convert.ToInt32(commandReader["ID"]);
                    user.FirstName = commandReader["FIRSTNAME"].ToString();
                    user.LastName = commandReader["LASTNAME"].ToString();
                    user.DateOfBirth = Convert.ToDateTime(commandReader["DATEOFBIRTH"]);
                                   
                }

                command.Connection.Close();

                return user;
                
            }
        }

    }
}
