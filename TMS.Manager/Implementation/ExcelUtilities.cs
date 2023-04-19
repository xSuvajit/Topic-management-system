using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using TMS.DTO;
using TMS.Manager.Contract;

namespace TMS.Manager.Implementation
{
    public class ExcelUtilities: IExcelUtilities
    {
        private string GetFileLocation()
        {
            var folder = Directory.GetCurrentDirectory();
            var p = Path.GetDirectoryName(folder);
            var r = Path.GetExtension(p);
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

        public string CreateExcel(Topics Data)
        {
            var postName1 = DateTime.Now.ToLongDateString().Split('/');
            var postName2 = DateTime.Now.ToLongTimeString().Split('/');
            string name = "Topics ";
            foreach (var str in postName1)
            {
                name += str;
            }
            name += " ";
            foreach (var str in postName2)
            {
                name += str;
            }
            name += ".xlsx";
            var fullname = GetFileLocation() + name;
            using (var document = SpreadsheetDocument.Create(fullname, SpreadsheetDocumentType.Workbook))
            {
                var workbookpart = document.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                var worksheetpart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetdata = new SheetData();
                worksheetpart.Worksheet = new Worksheet(sheetdata);
                var sheets = workbookpart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet()
                {
                    Id = workbookpart.GetIdOfPart(worksheetpart),
                    SheetId = 1,
                    Name = "First Work Sheet",
                };
                sheets.AppendChild(sheet);
                Columns? clms = worksheetpart.Worksheet.GetFirstChild<Columns>();
                bool needToInsertColumn = false;
                if (clms == null)
                {
                    clms = new Columns();
                    needToInsertColumn = true;
                }
                clms = AddColumns(clms, 8);
                if (needToInsertColumn)
                {
                    worksheetpart.Worksheet.InsertAt(clms, 0);
                }
                UInt32 rowindex = 0;
                var row = new Row() { RowIndex = ++rowindex };
                sheetdata.AppendChild(row);
                var index = 0;

                foreach (var header in Data.GetTopicsHeaders)
                {
                    var cell = new Cell() { CellValue = new CellValue(header), DataType = CellValues.String, CellReference = GetCellReference(++index) };
                    row.AppendChild(cell);
                }

                index = 0;

                foreach (var data in Data.GetTopics)
                {
                    row = new Row() { RowIndex = ++rowindex };
                    row.AppendChild(CreateCell(data.Id.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.MyTopics.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.Status.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.topicCode.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.createdBy.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.created.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.modifiedBy.ToString(), CellValues.String, ++index));
                    row.AppendChild(CreateCell(data.modified.ToString(), CellValues.String, ++index));
                    sheetdata.AppendChild(row);
                }
                
                workbookpart.Workbook.Save();
                return "success";
            }
        }

        private Cell CreateCell(string CellValue, CellValues type, int CellReference)
        {
            Cell cell = new Cell() { CellValue = new CellValue(CellValue), DataType = type, CellReference = GetCellReference(CellReference) };
            return cell;
        }
    }
}
