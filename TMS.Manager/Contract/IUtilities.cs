using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.DTO;

namespace TMS.Manager.Contract
{
    public interface IUtilities
    {
        string GetValue(string strCon);
        string GetSomeValue(string strCon);
        List<Topic> GetAllTopics(string strCon);
    }
}
