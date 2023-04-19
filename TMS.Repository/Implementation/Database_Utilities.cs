using System.Data;
using System.Data.SqlClient;
using TMS.DTO;
using TMS.Repository.Contract;

namespace TMS.Repository.Implementation
{
    public class Database_Utilities : IDatabase_Utilities
    {
        public string GetValue(string strCon, string sqlStr)
        {
            string op = "";
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon.State != ConnectionState.Open)
                {
                    sqlCon.Open();
                }
                using (SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlCon))
                {
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                op += reader.GetValue(i).ToString() + " ";
                            }
                            op += "\n";
                        }
                    }
                }
                if (sqlCon.State != ConnectionState.Closed)
                {
                    sqlCon.Close();
                }
            }
            return op;
        }

        public string GetSomeValue(string strCon, string sqlStr, int limit)
        {
            string op = "";
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon.State != ConnectionState.Open)
                {
                    sqlCon.Open();
                }
                using (SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlCon))
                {
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            for (int i = 0; i < limit; i++)
                            {
                                op += reader.GetValue(i).ToString() + " ";
                            }
                            op += "\n";
                        }
                    }
                }
                if (sqlCon.State != ConnectionState.Closed)
                {
                    sqlCon.Close();
                }
            }
            return op;
        }

        public List<Topic> GetAllTopics(string strCon, string sqlStr)
        {
            var listTopics= new List<Topic>();
            Topic? topic = null;
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon.State != ConnectionState.Open)
                {
                    sqlCon.Open();
                }
                using (SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlCon))
                {
                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        int id=reader.GetOrdinal("id");
                        int myTopic = reader.GetOrdinal("MyTopics");
                        int status=reader.GetOrdinal("Status");
                        int topicCode = reader.GetOrdinal("topicCode");
                        int createdBy = reader.GetOrdinal("createdBy");
                        int created = reader.GetOrdinal("created");
                        int modifiedBy = reader.GetOrdinal("modifiedBy");
                        int modified = reader.GetOrdinal("modified");
                        while (reader.Read())
                        {
                            topic = new Topic() {
                                Id = int.Parse(reader.GetValue(id).ToString()),
                                MyTopics = reader.GetValue(myTopic).ToString(),
                                Status = reader.GetValue(status).ToString(),
                                topicCode = int.Parse(reader.GetValue(topicCode).ToString()),
                                created = reader.GetDateTime(created),
                                createdBy = reader.GetValue(createdBy).ToString(),
                                modified = reader.GetDateTime(modified),
                                modifiedBy = reader.GetValue(modifiedBy).ToString()
                            };
                            listTopics.Add(topic);
                        }
                    }
                }
                if (sqlCon.State != ConnectionState.Closed)
                {
                    sqlCon.Close();
                }
            }
            return listTopics;
        }

    }
}