using DataAccessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DbConnection = DataAccessLayer.DbConnection;

namespace BusinessLayer
{
    public class MyUserBusinessCode : IUser
    {
        private readonly IConfiguration _configuration;
        public MyUserBusinessCode(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Users> GetUserData(int userId)
        {
            List<Users> users = new List<Users>();
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "SELECT * FROM Users WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Users user = new Users(
                     (int)reader["UserId"],
                     reader["UserName"].ToString(),
                     reader["Password"].ToString(),
                     reader["Name"].ToString(),
                     reader["Surname"].ToString(),
                     reader["SecurityQuestion"].ToString(),
                     reader["SecurityAnswer"].ToString(),
                     (DateTime)reader["DateOfRegister"]
                     );
                users.Add(user);
            }
            return users;
        }

        public bool AddUser(Users user)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "INSERT INTO Users (UserName, Password, Name, Surname, SecurityQuestion, SecurityAnswer, DateOfRegister) " +
                           "VALUES (@UserName, @Password, @Name, @Surname, @SecurityQuestion, @SecurityAnswer, @DateOfRegister)";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserName", user.userName);
            sqlCommand.Parameters.AddWithValue("@Password", user.password);
            sqlCommand.Parameters.AddWithValue("@Name", user.name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.surname);
            sqlCommand.Parameters.AddWithValue("@SecurityQuestion", user.securityQuestion);
            sqlCommand.Parameters.AddWithValue("@SecurityAnswer", user.securityAnswer);
            sqlCommand.Parameters.AddWithValue("@DateOfRegister", user.dateOfRegister);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public bool DeleteUser(int userId)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "DELETE FROM Users WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public bool UpdateUser(int userId, Users user)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "UPDATE Users SET " +
                           "UserName = @UserName, " +
                           "Password = @Password, " +
                           "Name = @Name, " +
                           "Surname = @Surname, " +
                           "SecurityQuestion = @SecurityQuestion, " +
                           "SecurityAnswer = @SecurityAnswer, " +
                           "DateOfRegister = @DateOfRegister " +
                           "WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);

            sqlCommand.Parameters.AddWithValue("@UserName", user.userName);
            sqlCommand.Parameters.AddWithValue("@Password", user.password);
            sqlCommand.Parameters.AddWithValue("@Name", user.name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.surname);
            sqlCommand.Parameters.AddWithValue("@SecurityQuestion", user.securityQuestion);
            sqlCommand.Parameters.AddWithValue("@SecurityAnswer", user.securityAnswer);
            sqlCommand.Parameters.AddWithValue("@DateOfRegister", user.dateOfRegister);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}