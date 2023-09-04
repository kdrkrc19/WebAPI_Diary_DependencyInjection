using DataAccessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MyDiaryBusinessCode : IDiary
    {
        private readonly IConfiguration _configuration;
        public MyDiaryBusinessCode(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddDiary(Diaries diary)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "INSERT INTO Diarys (Diary, Date, UpdateDate, UserId)" +
                           "VALUES (@Diary, @Date, @UpdateDate, @UserId)";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@Diary", diary.diary);
            sqlCommand.Parameters.AddWithValue("@Date", diary.date);
            sqlCommand.Parameters.AddWithValue("@UpdateDate", diary.updateDate);
            sqlCommand.Parameters.AddWithValue("@UserId", diary.userId);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public bool DeleteAllDiaries(int userId)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "DELETE FROM Diarys WHERE UserId = @UserID";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);          

            return (sqlCommand.ExecuteNonQuery() > 0);
        }

        public bool DeleteDiary(int diaryId, int userId)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "DELETE FROM Diarys WHERE DiaryId = @DiaryId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@DiaryId", diaryId);

            return sqlCommand.ExecuteNonQuery() > 0;
        }

        public List<Diaries> GetAllDiaries(int inputUserId)
        {
            List<Diaries> allDiaries = new List<Diaries>();
            MyDiaryBusinessCode diaryBusinessCode = new MyDiaryBusinessCode(_configuration);
            List<Diaries> diaryDataList = diaryBusinessCode.GetDiaryData(inputUserId);

            foreach (Diaries item in diaryDataList)
            {
                int diaryId = item.diaryId;
                int userId = item.userId;
                string diary = item.diary;
                DateTime date = item.date;
                DateTime updateDate = item.updateDate;

                allDiaries.Add(new Diaries(diaryId, diary, date, updateDate, userId));
            }

            return allDiaries;
        }

        public List<Diaries> GetDiaryData(int userId)
        {
            List<Diaries> diarys = new List<Diaries>();
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "SELECT * FROM Diarys WHERE UserId = @UserId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Diaries diary = new Diaries(
                    (int)reader["DiaryId"],
                    reader["Diary"].ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["UpdateDate"],
                    (int)reader["UserId"]
                    );
                diarys.Add(diary);
            }
            return diarys;
        }

        public bool UpdateDiary(int diaryId, Diaries diary)
        {
            DbConnection dbConnection = new DbConnection(_configuration);
            dbConnection.OpenConnection();

            string query = "UPDATE Diarys SET Diary = @Diary, Date = @Date, " +
                           "UpdateDate = @UpdateDate WHERE DiaryId = @DiaryId";

            SqlCommand sqlCommand = dbConnection.CreateCommand(query);

            sqlCommand.Parameters.AddWithValue("@Diary", diary.diary);
            sqlCommand.Parameters.AddWithValue("@Date", diary.date);
            sqlCommand.Parameters.AddWithValue("@UpdateDate", diary.updateDate);
            sqlCommand.Parameters.AddWithValue("@DiaryId", diaryId);

            return sqlCommand.ExecuteNonQuery() > 0;
        }
    }
}
