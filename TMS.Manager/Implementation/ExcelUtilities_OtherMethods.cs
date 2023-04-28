using DocumentFormat.OpenXml.Spreadsheet;
using TMS.Manager.Contract;

namespace TMS.Manager.Implementation
{
    public partial class ExcelUtilities : IExcelUtilities
    {
        private string GetFileLocation()
        {
            var folder = Directory.GetCurrentDirectory();
            //var p = Path.GetDirectoryName(folder);
            //var r = Path.GetExtension(p);
            var actualFullPath = folder.Split("\\");
            string destinationFullPath = string.Join(@"\", actualFullPath[0], actualFullPath[1], actualFullPath[2], @"Desktop\");
            return destinationFullPath;
        }

        private string GetCellReference(int index)
        {
            index--;

            if (index >= 0)
            {
                var fst = (index / 26) - 1;
                var snd = (index % 26);
                char fstChar = (char)(fst - 65);
                char sndChar = (char)(snd - 65);
                var ret = (fstChar == '@' ? ' ' : fstChar) + "" + (sndChar == '@' ? ' ' : sndChar);
                return ret.Trim();
            }
            else
            {
                throw new Exception("Invalid Column Index!!");
            }

        }

        private Columns AddColumns(Columns cols, int times)
        {
            for (int i = 1; i <= times; i++)
            {
                cols.Append(new Column() { Min = (UInt32)i, Max = (UInt32)i, Width = 18, CustomWidth = true });
            }
            return cols;
        }

        private Cell CreateCell(string CellValue, CellValues type, int CellReference)
        {
            Cell cell = new Cell() { CellValue = new CellValue(CellValue), DataType = type, CellReference = GetCellReference(CellReference) };
            return cell;
        }
    }
}
