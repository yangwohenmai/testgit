using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Utils
{
    public class ExcelUtil
    {

        /// <summary>
        /// 生成ExcelBook
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <param name="headers"></param>
        /// <param name="rows"></param>
        /// <param name="rowKeys"></param>
        /// <returns></returns>
        public static IWorkbook ExportWorkbook(string worksheetName, string[] headers, IQueryable rows, string[] rowKeys)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(worksheetName);

            //创建日期型单元格
            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //设置每一列的宽度
            int[] widths = SetColumnWidth(rows, headers, rowKeys);
            CreateNewSheet(rows, headers, rowKeys, workbook, sheet, widths);
            return workbook;
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="rows">行记录集</param>
        /// <param name="headers">标题头</param>
        /// <param name="rowKeys"></param>
        /// <returns></returns>
        public static int[] SetColumnWidth(IQueryable rows, string[] headers, string[] rowKeys)
        {
            int[] columnWidths = new int[headers.Length];
            for(int i = 0; i < headers.Length; ++i)
            {
                columnWidths[i] = Encoding.GetEncoding(936).GetBytes(headers[i]).Length;
            }
            foreach (object row in rows)
            {
                var type = row.GetType();
                for (int j = 0; j < rowKeys.Length; ++j)
                {
                    if (type.Name == "DataRow")
                    {
                        System.Data.DataRow dataRow = row as System.Data.DataRow;
                        string strValue = dataRow[j].ToString();
                        int intTemp = Encoding.GetEncoding(936).GetBytes(strValue).Length;
                        if (intTemp > columnWidths[j])
                        {
                            columnWidths[j] = intTemp > 254 ? 254 : intTemp;
                        }
                    }
                }
            }
            return columnWidths;
        }

        /// <summary>
        /// 生成sheet
        /// </summary>
        /// <param name="rows">数据集</param>
        /// <param name="headers">表头</param>
        /// <param name="rowKeys">（数据集）的待导出属性列S</param>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columnWidths">列宽</param>
        public static void CreateNewSheet(IQueryable rows, string[] headers, string[] rowKeys, IWorkbook workbook, ISheet sheet, int[] columnWidths)
        {
            int rowIndex = 0;
            foreach (object row in rows)
            {
                #region 设置表头
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }
                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        for (int i = 0; i < headers.Length; ++i)
                        {
                            headerRow.CreateCell(i).SetCellValue(headers[i]);
                            headerRow.GetCell(i).CellStyle = headStyle;
                            //设置列宽
                            sheet.SetColumnWidth(i, (columnWidths[i] + 1) * 256);
                        }
                    }
                    #endregion
                    rowIndex = 1;
                }
                #endregion

                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                var type = row.GetType();
                for (int j = 0; j < rowKeys.Length; ++j)
                {
                    ICell newCell = dataRow.CreateCell(j);
                    string drValue = "";
                    if (type.Name == "DataRow")
                    {
                        System.Data.DataRow dataRow1 = row as System.Data.DataRow;
                        drValue = dataRow1[j].ToString();
                    }

                    newCell.SetCellValue(drValue);
                }
                #endregion
                rowIndex++;
            }
            if (rowIndex == 0) {
                IRow headerRow = sheet.CreateRow(0);
                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 10;
                font.Boldweight = 700;
                headStyle.SetFont(font);
                for (int i = 0; i < headers.Length; ++i)
                {
                    headerRow.CreateCell(i).SetCellValue(headers[i]);
                    headerRow.GetCell(i).CellStyle = headStyle;
                    //设置列宽
                    sheet.SetColumnWidth(i, (columnWidths[i] + 1) * 256);
                }
            }
        }
    }
}
