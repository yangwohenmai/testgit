using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Qxun.App.Plugins.CarrierCore.Utils.Excel
{
    public class ExcelResult : ActionResult
    {
        /// <summary>
        /// File Name. 
        /// </summary>
        private string excelFileName;

        /// <summary>
        /// Sheet Name. 
        /// </summary>
        private string excelWorkSheetName;

        /// <summary>
        /// Excel Row data.
        /// </summary>
        private IQueryable rowData;

        /// <summary>
        /// Excel Header Data.
        /// </summary>
        private string[] headerData = null;

        /// <summary>
        /// Row Data Keys.
        /// </summary>
        private string[] rowPointers = null; 
        private string _fileName;
        private IWorkbook _result; 
        private static readonly string tempPath = "~/Upload/";

        /// <summary>
        /// Action Result: Excel result constructor for returning values from rows. 
        /// </summary>
        /// <param name="fileName">Excel file name.</param>
        /// <param name="workSheetName">Excel worksheet name: default: sheet1.</param>
        /// <param name="rows">Excel row values.</param>
        public ExcelResult(string fileName, string workSheetName, IQueryable rows)
            : this(fileName, workSheetName, rows, null, null)
        {
        }

        /// <summary>
        /// Action Result: Excel result constructor for returning values from rows and headers. 
        /// </summary>
        /// <param name="fileName">Excel file name.</param>
        /// <param name="workSheetName">Excel worksheet name: default: sheet1.</param>
        /// <param name="rows">Excel row values.</param>
        /// <param name="headers">Excel header values.</param>
        public ExcelResult(string fileName, string workSheetName, IQueryable rows, string[] headers)
            : this(fileName, workSheetName, rows, headers, null)
        {
        }

        /// <summary>
        /// Action Result: Excel result constructor for returning values from rows and headers and row keys. 
        /// </summary>
        /// <param name="fileName">Excel file name.</param>
        /// <param name="workSheetName">Excel worksheet name: default: sheet1.</param>
        /// <param name="rows">Excel row values.</param>
        /// <param name="headers">Excel header values.</param>
        /// <param name="rowKeys">Key values for the rows collection.</param>
        public ExcelResult(string fileName, string workSheetName, IQueryable rows, string[] headers, string[] rowKeys)
        {
            this.rowData = rows;
            this.excelFileName = fileName;
            this.excelWorkSheetName = workSheetName;
            this.headerData = headers;
            this.rowPointers = rowKeys;
        }

        public ExcelResult(string fileName, IWorkbook result)
        {
            _result = result;
            _fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        ///  Gets a value for file name.
        /// </summary>
        public string ExcelFileName
        {
            get { return this.excelFileName; }
        }

        /// <summary>
        ///  Gets a value for file name.
        /// </summary>
        public string ExcelWorkSheetName
        {
            get { return this.excelWorkSheetName; }
        }

        /// <summary>
        /// Gets a value for rows.
        /// </summary>
        public IQueryable ExcelRowData
        {
            get { return this.rowData; }
        }

        /// <summary>
        /// Execute the Excel Result. 
        /// </summary>
        /// <param name="context">Controller context.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            //MemoryStream stream = ExcelDocument.Create(this.excelFileName, this.excelWorkSheetName, this.rowData, this.headerData, this.rowPointers);
            //WriteStream(stream, this.excelFileName);
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (_result != null)
            {
                HttpResponseBase response = context.HttpContext.Response;
                string filePath = HttpContext.Current.Server.MapPath(tempPath + _fileName);
                FileStream sw = File.Create(filePath); //...
                _result.Write(sw);                              //...
                sw.Close();                                 //在服务端生成文件

                FileInfo file = new FileInfo(filePath);//文件保存路径及名称  
                //注意: 文件保存的父文件夹需添加Everyone用户，并给予其完全控制权限
                response.Clear();
                response.ClearHeaders();
                response.Buffer = false;
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName + ".xlsx", System.Text.Encoding.UTF8));
                response.AppendHeader("Content-Length", file.Length.ToString());
                response.WriteFile(file.FullName);
                response.Flush();                           //以上将生成的文件发送至用户浏览器
                File.Delete(filePath);                 //清除服务端生成的文件
            }
        }

        /// <summary>
        /// Writes the memory stream to the browser.
        /// </summary>
        /// <param name="memoryStream">Memory stream.</param>
        /// <param name="excelFileName">Excel file name.</param>
        private static void WriteStream(MemoryStream memoryStream, string excelFileName)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", excelFileName));
            memoryStream.WriteTo(context.Response.OutputStream);

            memoryStream.Close();
            context.Response.End();
        }
    }
}
