using DocumentFormat.OpenXml.Spreadsheet;
using TMS.DTO;

namespace TMS.Manager.Contract
{
    public interface IExcelUtilities
    {        
        string CreateExcel(Topics Data);
    }
}
