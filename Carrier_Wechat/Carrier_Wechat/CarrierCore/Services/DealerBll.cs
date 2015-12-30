using CarrierCore.Models;
using CarrierCore.ViewModels;
using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.App.Plugins.CarrierCore.Repositories;
using Qxun.App.Plugins.CarrierCore.Services;
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

namespace CarrierCore.Services
{
    public class DealerBll : BaseCarrierService<Dealer, ViewDealer, BaseCarrierRepository<Dealer>>
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
        public ListViewResponseResult<ViewDealer> GetDealerListPageList(int pageSize, int pageIndex, string platId, Expression<Func<Dealer, bool>> whereLambda, Expression<Func<Dealer, int>> orderByLambda = null, bool isAsc = false)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var listResult = GetMany(e => e.WeixinPlatId == weixinPlatId, pageIndex, pageSize, whereLambda, orderByLambda, isAsc);
            return listResult;
        }
        /// <summary>
        /// 获取所有经销商信息
        /// </summary>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public int GetDealerTotalCount(string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            return GetMany(o => o.WeixinPlatId == platId).Count;
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
        /// 获取Excel数据，批量插入
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
            //     string flag = string.Format("{0:MMddHHmmss}", DateTime.Now);
            //     HttpContext.Current.Session["weixinplat"] = wxPlat;
            string ErrorInfo = "";
            int errorCount = 0;
            Regex rex = new Regex(@"^\d+$");//判断第一行是数据还是表头，如果开头结尾都是数字的话，就是数据，否则是表头
            int first = 1;
            if (rex.IsMatch(data.Rows[0].ItemArray[0].ToString().Trim()))//如果ItemArray[6]匹配，说明起始行是数据，则从第零行开始读数据
                first = 0;
            Log4NHelper.Debug(string.Format("开始导入， first：{0}", first));//记录日志
            string BPCode, SiteName, SiteManager, Tel, SiteAddress, CompanyName, SiteType, SiteGrade, LocalManager, EstablishmentTime;
            ViewDealer info = new ViewDealer();
            SuccessResponseResult res = new SuccessResponseResult();
            int AllUpload = data.Rows.Count - 1;
            for (int line = first; line < data.Rows.Count; line++)//循环导入数据
            {
                try
                {
                    if (data.Rows[line].ItemArray[0].ToString().Length > 64)
                    {
                        BPCode = data.Rows[line].ItemArray[0].ToString();
                        BPCode = BPCode.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[0].ToString(), "BPCODE字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        BPCode = data.Rows[line].ItemArray[0].ToString();

                    if (data.Rows[line].ItemArray[1].ToString().Length > 64)
                    {
                        SiteName = data.Rows[line].ItemArray[1].ToString();
                        SiteName = SiteName.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[1].ToString(), "站点名称字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        SiteName = data.Rows[line].ItemArray[1].ToString();
                    if (data.Rows[line].ItemArray[2].ToString().Length > 64)
                    {
                        SiteManager = data.Rows[line].ItemArray[2].ToString();
                        SiteManager = SiteManager.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[2].ToString(), "站点负责人字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        SiteManager = data.Rows[line].ItemArray[2].ToString();
                    if (data.Rows[line].ItemArray[3].ToString().Length > 64)
                    {
                        Tel = data.Rows[line].ItemArray[3].ToString();
                        Tel = Tel.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[3].ToString(), "联系电话字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        Tel = data.Rows[line].ItemArray[3].ToString();
                    if (data.Rows[line].ItemArray[4].ToString().Length > 64)
                    {
                        SiteAddress = data.Rows[line].ItemArray[4].ToString();
                        SiteAddress = SiteAddress.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[4].ToString(), "服务站地址字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        SiteAddress = data.Rows[line].ItemArray[4].ToString();
                    if (data.Rows[line].ItemArray[5].ToString().Length > 64)
                    {
                        EstablishmentTime = data.Rows[line].ItemArray[5].ToString();
                        EstablishmentTime = EstablishmentTime.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[5].ToString(), "创建时间字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        EstablishmentTime = data.Rows[line].ItemArray[5].ToString();
                    if (data.Rows[line].ItemArray[6].ToString().Length > 64)
                    {
                        CompanyName = data.Rows[line].ItemArray[6].ToString();
                        CompanyName = CompanyName.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[6].ToString(), "公司名称字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        CompanyName = data.Rows[line].ItemArray[6].ToString();
                    if (data.Rows[line].ItemArray[7].ToString().Length > 64)
                    {
                        SiteType = data.Rows[line].ItemArray[7].ToString();
                        SiteType = SiteType.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[7].ToString(), "站点类型字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        SiteType = data.Rows[line].ItemArray[7].ToString();
                    if (data.Rows[line].ItemArray[8].ToString().Length > 64)
                    {
                        SiteGrade = data.Rows[line].ItemArray[8].ToString();
                        SiteGrade = SiteGrade.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[8].ToString(), "站点等级字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        SiteGrade = data.Rows[line].ItemArray[8].ToString();
                    if (data.Rows[line].ItemArray[9].ToString().Length > 64)
                    {
                        LocalManager = data.Rows[line].ItemArray[9].ToString();
                        LocalManager = LocalManager.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[9].ToString(), "区域服务经理字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        LocalManager = data.Rows[line].ItemArray[9].ToString();
                    
                    
                        //FactoryName = data.Rows[line].ItemArray[0].ToString(),
                        //UserNumber = data.Rows[line].ItemArray[1].ToString(),
                        //UserFirstName = data.Rows[line].ItemArray[2].ToString(),
                        //UserSecondName = data.Rows[line].ItemArray[3].ToString(),
                    info.BPCode = BPCode;
                    info.SiteName = SiteName;
                    info.SiteManager = SiteManager;
                    info.Tel = Tel;
                    info.SiteAddress = SiteAddress;
                    info.CompanyName = CompanyName;
                    info.SiteType = SiteType;
                    info.SiteGrade = SiteGrade;
                    info.LocalManager = LocalManager;
                    info.EstablishmentTime = Convert.ToDateTime(EstablishmentTime);
                    //DeadLine = DateTime.Now,//计算得出
                    info.WeixinPlatId = wxPlat.WeixinPlatId;
                    
                    res = InsertDealerInfoBig(info, wxPlat.WeixinPlatId);//插入数据，并返回是否成功
                    if (res.IsSuccess == false)//如果插入失败，记录日志
                    {
                        Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.BPCode + "," + info.SiteName + "," + info.SiteManager + "," + info.Tel + "," + info.SiteAddress + "," + info.CompanyName + "," + info.SiteType + "," + info.SiteGrade + "," + info.LocalManager + "," + info.EstablishmentTime));//记录错误信息
                        ++errorCount;
                        ErrorInfo += errorCount.ToString() + ",";
                    }
                }
                catch (Exception)
                {
                    res.ErrorMsg = "待导入.csv文件中数据字段少于列表中数据字段";
                    Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.BPCode + "," + info.SiteName + "," + info.SiteManager + "," + info.Tel + "," + info.SiteAddress + "," + info.CompanyName + "," + info.SiteType + "," + info.SiteGrade + "," + info.LocalManager + "," + info.EstablishmentTime));//记录错误信息
                    ++errorCount;
                    ErrorInfo += errorCount.ToString() + ",";
                    continue;
                }

            }
            if (ErrorInfo.Length > 0)
            {
                ErrorInfo = ErrorInfo.TrimEnd(',');
            }
            new RecordInfoBll().InsertError(errorCount, AllUpload, ErrorInfo, wxPlat.WeixinPlatId, RecordType.Dealer);
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
        
        public SuccessResponseResult InsertDealerInfoBig(ViewDealer Dealer, string weixinPlateId)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            int platId = DesDecodeKey(weixinPlateId);
            //ViewDealer PartInfoEntity1 = Get(o => o.PartNumber == Dealer.PartNumber && o.WeixinPlatId == platId);
            if (Dealer.BPCode.Length == 0 || Dealer.SiteName.Length == 0 || Dealer.SiteManager.Length == 0 || Dealer.Tel.Length == 0 || Dealer.SiteAddress.Length == 0 || Dealer.CompanyName.Length == 0 || Dealer.SiteType.Length == 0 || Dealer.SiteGrade.Length == 0 || Dealer.LocalManager.Length == 0 || Dealer.EstablishmentTime.ToString().Length == 0 )
            {
                result.IsSuccess = false;
                result.ResponseCode = ResponseCode.NoData;
                result.ErrorMsg = "数据中有不完整的数据，该数据插入失败，请将数据补充完整！";
                return result;
            }
            if (Dealer != null)//如果经销商号不同，直接插入，如果机组号相同，整体更新
            {

                ViewDealer DealerInfoEntity = GetDealerNumBig(Dealer.BPCode, weixinPlateId);//通过经销商编号查找是否已经有这样的记录
                if (DealerInfoEntity != null)//如果有这样的记录，直接全部更新数据
                {

                    result = UpdateEntity(DealerInfoEntity.DealerId, e => { e.BPCode = Dealer.BPCode; e.SiteName = Dealer.SiteName; e.SiteManager = Dealer.SiteManager; e.Tel = Dealer.Tel; e.SiteAddress = Dealer.SiteAddress; e.CompanyName = Dealer.CompanyName; e.SiteType = Dealer.SiteType; e.SiteGrade = Dealer.SiteGrade; e.LocalManager = Dealer.LocalManager; e.EstablishmentTime = Dealer.EstablishmentTime; e.WeixinPlatId = weixinPlateId; });
                }
                else//经销商号不存在，直接插入
                {
                    result = InsertEntity(Dealer);
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
        /// 根据BP号查出实体
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <param name="FactoryName"></param>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public ViewDealer GetDealerNumBig(string BPCode, string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            if (!string.IsNullOrEmpty(BPCode))
                return Get(o => o.BPCode == BPCode && o.WeixinPlatId == platId);
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
        public void ExportDealerinfoInit(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<Dealer, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewDealer> list = GetDealers(whereLambda);

            DataTable table = new DataTable();
            List<string> headerTitles = new List<string> { "BPCODE", "站点名称", "站点负责人","联系电话","站点地址","站点建立时间","公司名称","站点类型","站点等级","区域经理" };
            List<string> exportFields = new List<string> { "BPCode", "SiteName", "SiteManager", "Tel", "SiteAddress", "EstablishmentTime", "CompanyName", "SiteType", "SiteGrade", "LocalManager" };
            table.Columns.Add("BPCode", typeof(string));
            table.Columns.Add("SiteName", typeof(string));
            table.Columns.Add("SiteManager", typeof(decimal));
            table.Columns.Add("Tel", typeof(string));
            table.Columns.Add("SiteAddress", typeof(string));
            table.Columns.Add("EstablishmentTime", typeof(string));
            table.Columns.Add("CompanyName", typeof(string));
            table.Columns.Add("SiteType", typeof(string));
            table.Columns.Add("SiteGrade", typeof(string));
            table.Columns.Add("LocalManager", typeof(string));
            if (list.Count > 0)
            {
                foreach (var dealer in list)
                {
                    DataRow row = table.NewRow();
                    row["BPCode"] = dealer.BPCode;
                    row["SiteName"] = dealer.SiteName;
                    row["SiteManager"] = dealer.SiteManager;
                    row["Tel"] = dealer.Tel;
                    row["SiteAddress"] = dealer.SiteAddress;
                    row["EstablishmentTime"] = dealer.EstablishmentTime;
                    row["CompanyName"] = dealer.CompanyName;
                    row["SiteType"] = dealer.SiteType;
                    row["SiteGrade"] = dealer.SiteGrade;
                    row["LocalManager"] = dealer.LocalManager;
                    table.Rows.Add(row);
                }
            }

            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        #region EPR导出
        public void ExportUserinfoInitERP(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<Dealer, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewDealer> list = GetDealers(whereLambda);

            DataTable table = new DataTable();
            List<string> headerTitles = new List<string> { "BPCODE", "站点名称", "站点负责人", "联系电话", "站点地址", "站点建立时间", "公司名称", "站点类型" };
            List<string> exportFields = new List<string> { "BPCode", "SiteName", "SiteManager", "Tel", "SiteAddress", "EstablishmentTime", "CompanyName", "SiteType" };
            table.Columns.Add("BPCode", typeof(string));
            table.Columns.Add("SiteName", typeof(string));
            table.Columns.Add("SiteManager", typeof(decimal));
            table.Columns.Add("Tel", typeof(string));
            table.Columns.Add("SiteAddress", typeof(string));
            table.Columns.Add("EstablishmentTime", typeof(string));
            table.Columns.Add("CompanyName", typeof(string));
            table.Columns.Add("SiteType", typeof(string));
            table.Columns.Add("SiteGrade", typeof(string));
            table.Columns.Add("LocalManager", typeof(string));
            if (list.Count > 0)
            {
                foreach (var dealer in list)
                {
                    DataRow row = table.NewRow();
                    row["BPCode"] = dealer.BPCode;
                    row["SiteName"] = dealer.SiteName;
                    row["SiteManager"] = dealer.SiteManager;
                    row["Tel"] = dealer.Tel;
                    row["SiteAddress"] = dealer.SiteAddress;
                    row["EstablishmentTime"] = dealer.EstablishmentTime;
                    row["CompanyName"] = dealer.CompanyName;
                    row["SiteType"] = dealer.SiteType;
                    row["SiteGrade"] = dealer.SiteGrade;
                    row["LocalManager"] = dealer.LocalManager;
                    table.Rows.Add(row);
                }
            }
            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        public IList<ViewDealer> GetDealers(Expression<Func<Dealer, bool>> whereLambda)
        {
            return GetMany(whereLambda);
        }
       
    }
}
