using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using TMS.DTO;
using TMS.Manager.Contract;

namespace TMS.Manager.Implementation
{
    public partial class ExcelUtilities: IExcelUtilities
    {
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

        
    }
}
