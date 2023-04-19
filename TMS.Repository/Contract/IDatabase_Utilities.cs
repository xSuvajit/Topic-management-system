using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
