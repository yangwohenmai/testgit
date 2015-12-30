using Qxun.App.Plugins.CarrierCore.Models;
using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.App.Plugins.CarrierCore.Repositories;
using Qxun.App.Plugins.CarrierCore.ViewModels;
using Qxun.App.Weistone.Services;
using Qxun.App.Weistone.ViewModels;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

namespace Qxun.App.Plugins.CarrierCore.Services
{
    public class PartInformationBll : BaseCarrierService<PartInformation, ViewPartInformation, BaseCarrierRepository<PartInformation>>
    {
        /// <summary>
        /// 获取实体的分页列表数据 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="platId"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public ListViewResponseResult<ViewPartInformation> GetPartInformationListPageList(int pageSize, int pageIndex, string platId, Expression<Func<PartInformation, bool>> whereLambda, Expression<Func<PartInformation, int>> orderByLambda = null, bool isAsc = false)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var listResult = GetMany(e => e.WeixinPlatId == weixinPlatId, pageIndex, pageSize, whereLambda, orderByLambda, isAsc);
            return listResult;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public int GetPartInformationTotalCount(string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            return GetMany(o => o.WeixinPlatId == platId).Count;
        }
        /// <summary>
        /// 重新的函数
        /// </summary>
        /// <param name="platId"></param>
        /// <param name="PartNumber"></param>
        /// <returns></returns>
        public SuccessResponseResult GetPartInfo(string platId, string PartNumber)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var result = Get(e => e.WeixinPlatId == weixinPlatId && e.PartNumber == PartNumber);
            return ToSuccessResponseResult(result);
        }



        #region 批量导入
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="file"></param>
        /// <param name="weixinPlat"></param>
        /// <returns></returns>
        public SuccessResponseResult BatImportFromFile(HttpPostedFileBase file, ViewWeixinPlat weixinPlat)
        {
            SuccessResponseResult result = new SuccessResponseResult { IsSuccess = true, ResponseCode = ResponseCode.Success };
            Log4NHelper.Debug("开始批量更新数据");//记录日志
            CSVHelper getCSV = new CSVHelper();
            DataTable csvTable = new DataTable();
            try
            {
                csvTable = getCSV.ReadCsv(file);//把Excel导入Data表格
            }
            catch (Exception ex)
            {
                Log4NHelper.Error(ex);
                result.IsSuccess = false;
                result.ErrorMsg = "格式解析错误，请对照模板检查格式后重新上传";
                return result;
            }
            Log4NHelper.Debug(string.Format("Rows: {0}", csvTable.Rows.Count));
            if (csvTable.Rows.Count > 5001)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "批量上传数量量最多为5000条，超过5000条请分批次上传";
                return result;
            }
            else
            {
                ParameterizedThreadStart ImportCVSThreadStart = new ParameterizedThreadStart((obj) =>
                {
                    TextWriter tw = new StringWriter();
                    HttpWorkerRequest wr = new SimpleWorkerRequest("", "", tw);
                    HttpContext.Current = new HttpContext(wr);
                    System.Web.SessionState.SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, new HttpSessionStateContainer("", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 20000, true, HttpCookieMode.UseCookies, SessionStateMode.Off, false));
                    object[] objs = obj as object[];
                    BatchInsert(objs[0] as DataTable, objs[1] as ViewWeixinPlat);
                });

                Thread NEWimport = new Thread(ImportCVSThreadStart);
                NEWimport.Start(new object[] { csvTable, weixinPlat });
                //  BatchInsert(csvTable, weixinPlat);
            }
            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="wxPlat"></param>
        private void BatchInsert(DataTable data, ViewWeixinPlat wxPlat)
        {
            AccountBll accountBll = new AccountBll();
            ViewAccount account = new ViewAccount();
            account.HasAutorityAccountIds = accountBll.GetAllAccountId();
            HttpContext.Current.Session["user"] = account;
            HttpContext.Current.Session["weixinplat"] = wxPlat;
            ViewPartInformation info = new ViewPartInformation();
            SuccessResponseResult res = new SuccessResponseResult();
            string ErrorInfo = "";
            int errorCount = 0;
            Regex rex = new Regex(@"^\d+$");//判断第一行是数据还是表头，如果开头结尾都是数字的话，就是数据，否则是表头
            int first = 1;
            if (rex.IsMatch(data.Rows[0].ItemArray[0].ToString().Trim()))//如果ItemArray[6]匹配，说明起始行是数据，则从第零行开始读数据
                first = 0;
            Log4NHelper.Debug(string.Format("开始导入， first：{0}", first));//记录日志
            string PartNumber, PartName, Price ;
            int AllUpload = data.Rows.Count - 1;
            for (int line = first; line < data.Rows.Count; line++)//循环导入数据
            {
                try
                {
                    if (data.Rows[line].ItemArray[0].ToString().Length > 64)
                    {
                        PartNumber = data.Rows[line].ItemArray[0].ToString();
                        PartNumber = PartNumber.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[0].ToString(), "零件编号字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        PartNumber = data.Rows[line].ItemArray[0].ToString();

                    if (data.Rows[line].ItemArray[1].ToString().Length > 64)
                    {
                        PartName = data.Rows[line].ItemArray[1].ToString();
                        PartName = PartName.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[1].ToString(), "零件名字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        PartName = data.Rows[line].ItemArray[1].ToString();
                    if (data.Rows[line].ItemArray[2].ToString().Length > 64)
                    {
                        Price = data.Rows[line].ItemArray[2].ToString();
                        Price = Price.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[2].ToString(), "零件价格字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        Price = data.Rows[line].ItemArray[2].ToString();

                    info.PartNumber = PartNumber;
                    info.PartName = PartName;
                    info.Price = Convert.ToDecimal(Price);
                    //ProduceTime = Convert.ToDateTime(strProduceTime),
                    //InstallTime = Convert.ToDateTime(strInstallTime),
                    //QualityGuaranteePeriod = strQualityGuaranteePeriod,
                    //DeadLine = DateTime.Now,//计算得出
                    info.WeixinPlatId = wxPlat.WeixinPlatId;
                    
                    res = InsertPartInfoBig(info, wxPlat.WeixinPlatId);//插入数据，并返回是否成功
                    if (res.IsSuccess == false)//如果插入失败，记录日志
                    {
                        Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.PartName + "," + info.PartNumber + "," + info.Price.ToString()));//记录错误信息
                        ++errorCount;
                        ErrorInfo += errorCount.ToString() + ",";
                    }
                }
                catch (Exception)
                {
                    res.ErrorMsg = "待导入.csv文件中数据字段少于列表中数据字段";
                    Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.PartName + "," + info.PartNumber + "," + info.Price.ToString()));//记录错误信息
                    ++errorCount;
                    ErrorInfo += errorCount.ToString() + ",";
                    continue;
                }

            }
            if (ErrorInfo.Length > 0)
            {
                ErrorInfo = ErrorInfo.TrimEnd(',');
            }
            new RecordInfoBll().InsertError(errorCount, AllUpload, ErrorInfo, wxPlat.WeixinPlatId, RecordType.Part);
            Log4NHelper.Debug(string.Format("成功导入{0}条， {1}条导入时发生了错误！", data.Rows.Count - errorCount - first, errorCount));//记录错误信息
            HttpContext.Current.Session["user"] = null;
            HttpContext.Current.Session["weixinplat"] = null;

        }


        /// <summary>
        /// 插入或更新判断
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <param name="weixinPlateId"></param>
        /// <returns></returns>
        public SuccessResponseResult InsertPartInfoBig(ViewPartInformation PartInformation, string weixinPlateId)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            int platId = DesDecodeKey(weixinPlateId);
            ViewPartInformation PartInfo = Get(o => o.PartNumber == PartInformation.PartNumber && o.WeixinPlatId == platId);
            if (PartInformation.PartName.Length == 0 || PartInformation.PartNumber.Length == 0 || PartInformation.Price.ToString().Length == 0 )
            {
                result.IsSuccess = false;
                result.ResponseCode = ResponseCode.NoData;
                result.ErrorMsg = "数据中有不完整的数据，该数据插入失败，请将数据补充完整！";
                return result;
            }
            if (PartInformation != null)//如果零件号不同，直接插入，如果零件号相同，整体更新
            {

                ViewPartInformation PartInfoEntity = GetPartNumBig(PartInformation.PartNumber, weixinPlateId);//通过零件编号查找是否已经有这样的记录
                if (PartInfoEntity != null)//如果有这样的记录，直接全部更新数据
                {

                    result = UpdateEntity(PartInfoEntity.PartId, e => { e.PartNumber = PartInformation.PartNumber; e.PartName = PartInformation.PartName; e.Price = PartInformation.Price; e.WeixinPlatId = weixinPlateId; e.PartAvata = PartInfo.PartAvata; });
                }
                else//零件号不存在，直接插入
                {
                    result = InsertEntity(PartInformation);
                }
            }
            else
            {
                result.IsSuccess = false;
                result.ResponseCode = ResponseCode.NoData;
                result.ErrorMsg = "插入空对象";
            }
            return result;
        }

        /// <summary>
        /// 根据零件号获取实体
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <param name="FactoryName"></param>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public ViewPartInformation GetPartNumBig(string PartNumber, string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            //string another = "StringVaule = " + UserNumber;
            if (!string.IsNullOrEmpty(PartNumber))
                return Get(o => o.PartNumber == PartNumber && o.WeixinPlatId == platId);
            else
                return null;
        }




        /// <summary>
        /// 根据姓名获取实体
        /// </summary>
        /// <param name="UserFirstName"></param>
        /// <param name="UserSecondName"></param>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        //public ViewUnitInformation UserElseBig(string UserFirstName, string UserSecondName, string weixinPlatId)
        //{
        //    int platId = DesDecodeKey(weixinPlatId);
        //    //string another = "StringVaule = " + UserNumber;
        //    if (!string.IsNullOrEmpty(UserFirstName))
        //        return Get(o => o.UserFirstName == UserFirstName && o.WeixinPlatId == platId && o.UserSecondName == UserSecondName);
        //    else
        //        return null;
        //}
        #endregion


        #region 全部导出
        public void ExportPartinfoInit(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<PartInformation, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewPartInformation> list = GetPart(whereLambda);

            DataTable table = new DataTable();
            List<string> headerTitles = new List<string> { "零件号", "零件名称", "价格" };
            List<string> exportFields = new List<string> { "PartNumber", "PartName", "Price" };
            table.Columns.Add("PartNumber", typeof(string));
            table.Columns.Add("PartName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            if (list.Count > 0)
            {
                foreach (var part in list)
                {
                    DataRow row = table.NewRow();
                    row["PartNumber"] = part.PartNumber;
                    row["PartName"] = part.PartName;
                    row["Price"] = part.Price;
                    table.Rows.Add(row);
                }
            }

            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        #region EPR导出
        public void ExportPartinfoInitERP(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<PartInformation, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewPartInformation> list = GetPart(whereLambda);

            DataTable table = new DataTable();
            List<string> headerTitles = new List<string> { "零件号", "零件名称", "价格" };
            List<string> exportFields = new List<string> { "PartNumber", "PartName", "Price" };
            table.Columns.Add("PartNumber", typeof(string));
            table.Columns.Add("PartName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            if (list.Count > 0)
            {
                foreach (var part in list)
                {
                    DataRow row = table.NewRow();
                    row["PartNumber"] = part.PartNumber;
                    row["PartName"] = part.PartName;
                    row["Price"] = part.Price;
                    table.Rows.Add(row);
                }
            }

            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        public IList<ViewPartInformation> GetPart(Expression<Func<PartInformation, bool>> whereLambda)
        {
            return GetMany(whereLambda);
        }



    }
}
