using TMS.DTO;
using TMS.Manager.Contract;
using TMS.Repository.Contract;

namespace TMS.Manager.Implementation
{
    public class Utilities: IUtilities
    {
        private readonly IDatabase_Utilities database;
        public Utilities(IDatabase_Utilities _database)
        {
            database = _database;
        }

        public string GetValue(string strCon)
        {
            string sqlStr = "select * from [dbo].[Topics]";
            return database.GetValue(strCon, sqlStr);
        }

        public string GetSomeValue(string strCon)
        {
            string sqlStr = "select MyTopics,Status,topicCode from [dbo].[Topics]";
            return database.GetSomeValue(strCon, sqlStr,3);
        }

        public List<Topic> GetAllTopics(string strCon)
        {
            string sqlStr = "select * from [dbo].[Topics]";
            return database.GetAllTopics(strCon,sqlStr);
        }



    }
}
