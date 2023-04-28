using TMS.DTO;

namespace TMS.Repository.Contract
{
    public interface IDatabase_Utilities
    {
        string GetValue(string strCon, string sqlStr);
        string GetSomeValue(string strCon, string sqlStr, int limit);
        List<Topic> GetAllTopics(string strCon, string sqlStr);
    }
}
