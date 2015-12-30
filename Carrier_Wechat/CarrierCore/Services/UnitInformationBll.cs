using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.App.Plugins.CarrierCore.Models;
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
using Qxun.App.Plugins.CarrierCore.Public;

namespace Qxun.App.Plugins.CarrierCore.Services
{
    public class UnitInformationBll : BaseCarrierCoreService<UnitInformation, ViewUnitInformation, BaseCarrierCoreRepository<UnitInformation>>
    {
        public ListViewResponseResult<ViewUnitInformation> GetUserListPageList(int page, int pageSize, int pageIndex, string platId, Expression<Func<UnitInformation, bool>> whereLambda)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var listResult = GetMany(e => e.WeixinPlatId == weixinPlatId, pageIndex, pageSize, whereLambda, e => e.UnitNumber, false);
            return listResult;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public int GetUnitInformationTotalCount(string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            return GetMany(o => o.WeixinPlatId == platId).Count;
        }
        /// <summary>
        /// 根据机组序列号获取机组信息
        /// </summary>
        /// <param name="platId"></param>
        /// <param name="PartNumber"></param>
        /// <returns></returns>
        public SuccessResponseResult GetUnitInfo(string platId, string UnitNumber)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var result = Get(e => e.WeixinPlatId == weixinPlatId && e.UnitNumber == UnitNumber);
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
            ViewUnitInformation info = new ViewUnitInformation();
            SuccessResponseResult res = new SuccessResponseResult();
            //     string flag = string.Format("{0:MMddHHmmss}", DateTime.Now);
            //     HttpContext.Current.Session["weixinplat"] = wxPlat;
            string ErrorInfo = "";
            int errorCount = 0;
            Regex rex = new Regex(@"^\d+$");//判断第一行是数据还是表头，如果开头结尾都是数字的话，就是数据，否则是表头
            int first = 1;
            if (rex.IsMatch(data.Rows[0].ItemArray[0].ToString().Trim()))//如果ItemArray[6]匹配，说明起始行是数据，则从第零行开始读数据
                first = 0;
            Log4NHelper.Debug(string.Format("开始导入， first：{0}", first));//记录日志
            string strUnitName, strUnitNumber, strSpecifications, strUnitProducer, strUnitType, strProduceTime, strInstallTime, strUnitModel, strDealerNumber, strQualityGuaranteePeriod;
            int AllUpload = data.Rows.Count - 1;
            for (int line = first; line < data.Rows.Count; line++)//循环导入数据
            {
                try
                {
                    if (data.Rows[line].ItemArray[0].ToString().Length > 64)
                    {
                        strUnitName = data.Rows[line].ItemArray[0].ToString();
                        strUnitName = strUnitName.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[0].ToString(), "机组名称字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        strUnitName = data.Rows[line].ItemArray[0].ToString();

                    if (data.Rows[line].ItemArray[1].ToString().Length > 64)
                    {
                        strUnitNumber = data.Rows[line].ItemArray[1].ToString();
                        strUnitNumber = strUnitNumber.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[1].ToString(), "机组序号字符串过长，只截取前32位"));//记录错误信息
                    }
                    else
                        strUnitNumber = data.Rows[line].ItemArray[1].ToString();
                    if (data.Rows[line].ItemArray[2].ToString().Length > 64)
                    {
                        strSpecifications = data.Rows[line].ItemArray[2].ToString();
                        strSpecifications = strSpecifications.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[2].ToString(), "机组规格字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strSpecifications = data.Rows[line].ItemArray[2].ToString();
                    if (data.Rows[line].ItemArray[3].ToString().Length > 64)
                    {
                        strUnitProducer = data.Rows[line].ItemArray[3].ToString();
                        strUnitProducer = strUnitProducer.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[3].ToString(), "机组生产商字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strUnitProducer = data.Rows[line].ItemArray[3].ToString();
                    if (data.Rows[line].ItemArray[4].ToString().Length > 64)
                    {
                        strUnitType = data.Rows[line].ItemArray[4].ToString();
                        strUnitType = strUnitType.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[4].ToString(), "机组类型字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strUnitType = data.Rows[line].ItemArray[4].ToString();
                    if (data.Rows[line].ItemArray[5].ToString().Length > 64)
                    {
                        strProduceTime = data.Rows[line].ItemArray[5].ToString();
                        strProduceTime = strProduceTime.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[5].ToString(), "机组生产日期字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strProduceTime = data.Rows[line].ItemArray[5].ToString();
                    if (data.Rows[line].ItemArray[6].ToString().Length > 64)
                    {
                        strInstallTime = data.Rows[line].ItemArray[6].ToString();
                        strInstallTime = strInstallTime.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[6].ToString(), "机组安装日期字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strInstallTime = data.Rows[line].ItemArray[6].ToString();
                    if (data.Rows[line].ItemArray[7].ToString().Length > 64)
                    {
                        strUnitModel = data.Rows[line].ItemArray[7].ToString();
                        strUnitModel = strUnitModel.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[7].ToString(), "机组型号字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strUnitModel = data.Rows[line].ItemArray[7].ToString();
                    if (data.Rows[line].ItemArray[8].ToString().Length > 64)
                    {
                        strDealerNumber = data.Rows[line].ItemArray[8].ToString();
                        strDealerNumber = strDealerNumber.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[8].ToString(), "经销商代码字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strDealerNumber = data.Rows[line].ItemArray[8].ToString();
                    if (data.Rows[line].ItemArray[9].ToString().Length > 64)
                    {
                        strQualityGuaranteePeriod = data.Rows[line].ItemArray[9].ToString();
                        strQualityGuaranteePeriod = strQualityGuaranteePeriod.Substring(0, 64);
                        Log4NHelper.Debug(string.Format("BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, data.Rows[line].ItemArray[9].ToString(), "保修期字符串过长，只截取前64位"));//记录错误信息
                    }
                    else
                        strQualityGuaranteePeriod = data.Rows[line].ItemArray[9].ToString();
                    

                        //FactoryName = data.Rows[line].ItemArray[0].ToString(),
                        //UserNumber = data.Rows[line].ItemArray[1].ToString(),
                        //UserFirstName = data.Rows[line].ItemArray[2].ToString(),
                        //UserSecondName = data.Rows[line].ItemArray[3].ToString(),
                    info.UnitName = strUnitName;
                    info.UnitNumber = strUnitNumber;
                    info.Specifications = strSpecifications;
                    info.UnitProducer = strUnitProducer;
                    info.UnitType = strUnitType;
                    info.ProduceTime = Convert.ToDateTime(strProduceTime);
                    info.InstallTime = Convert.ToDateTime(strInstallTime);
                    info.UnitModel = strUnitModel;
                    info.DealerNumber = strDealerNumber;
                    info.QualityGuaranteePeriod = strQualityGuaranteePeriod;
                    info.DeadLine = DateTime.Now;//计算得出
                    info.WeixinPlatId = wxPlat.WeixinPlatId;

                    res = InsertUserInfoBig(info, wxPlat.WeixinPlatId);//插入数据，并返回是否成功
                    if (res.IsSuccess == false)//如果插入失败，记录日志
                    {
                        Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.UnitName + "," + info.UnitNumber + "," + info.Specifications + "," + info.UnitProducer + "," + info.UnitType + "," + info.ProduceTime + "," + info.InstallTime + "," + info.UnitModel + "," + info.DealerNumber + "," + info.QualityGuaranteePeriod));//记录错误信息
                        ++errorCount;
                        ErrorInfo +=  errorCount.ToString() + ",";
                    }
                }
                catch (Exception)
                {
                    res.ErrorMsg = "待导入.csv文件中数据字段少于列表中数据字段";
                    Log4NHelper.Debug(string.Format("Rows[{0}] BatchInsert Error, ErrorMsg: {1}. Data:{2}", line, res.ErrorMsg, info.UnitName + "," + info.UnitNumber + "," + info.Specifications + "," + info.UnitProducer + "," + info.UnitType + "," + info.ProduceTime + "," + info.InstallTime + "," + info.UnitModel + "," + info.DealerNumber + "," + info.QualityGuaranteePeriod));//记录错误信息
                    ++errorCount;
                    ErrorInfo += errorCount.ToString() + ",";
                    continue;
                }

            }
            if (ErrorInfo.Length>0)
            {
                ErrorInfo = ErrorInfo.TrimEnd(',');
            }
            new RecordInfoBll().InsertError(errorCount, AllUpload, ErrorInfo, wxPlat.WeixinPlatId, RecordType.Unit);
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
        public SuccessResponseResult InsertUserInfoBig(ViewUnitInformation UnitInformation, string weixinPlateId)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            
            if (UnitInformation.UnitName.Length == 0 || UnitInformation.UnitNumber.Length == 0 || UnitInformation.Specifications.Length == 0 || UnitInformation.UnitProducer.Length == 0 || UnitInformation.UnitType.Length == 0 || UnitInformation.ProduceTime.ToString().Length == 0 || UnitInformation.InstallTime.ToString().Length == 0 || UnitInformation.UnitModel.Length == 0 || UnitInformation.DealerNumber.Length == 0 || UnitInformation.QualityGuaranteePeriod.Length == 0)
            {
                result.IsSuccess = false;
                result.ResponseCode = ResponseCode.NoData;
                result.ErrorMsg = "数据中有不完整的数据，该数据插入失败，请将数据补充完整！";
                return result;
            }
            if (UnitInformation != null)//如果机组号不同，直接插入，如果机组号相同，整体更新
            {
                ViewUnitInformation userInfoEntity = GetUserNumBig(UnitInformation.UnitNumber, weixinPlateId);//通过机组编号查找是否已经有这样的记录
                if (userInfoEntity != null)//如果有这样的记录，直接全部更新数据
                {
                    result = UpdateEntity(userInfoEntity.UnitId, e => { e.UnitName = UnitInformation.UnitName; e.Specifications = UnitInformation.Specifications; e.UnitProducer = UnitInformation.UnitProducer; e.UnitType = UnitInformation.UnitType; e.ProduceTime = UnitInformation.ProduceTime; e.InstallTime = UnitInformation.InstallTime; e.UnitModel = UnitInformation.UnitModel; e.DealerNumber = UnitInformation.DealerNumber; e.QualityGuaranteePeriod = UnitInformation.QualityGuaranteePeriod; e.DeadLine = UnitInformation.DeadLine; });
                    //result.IsSuccess = true;
                    //ViewUnitInformation checkElse = UserElseBig(UnitInformation.UserFirstName, UnitInformation.UserSecondName, weixinPlateId);
                    //if (checkElse != null)//所有信息都相同，说明数据已经存在，不再插入
                    //{
                    //    result.IsSuccess = false;
                    //    result.ErrorMsg = "该员工数据已存在！";
                    //}
                    //else//姓名等不同，说明旧号给了新人，要修改，或者先删再从新插入
                    //{
                    //    UpdateEntity(userInfoEntity.UnitId, e => { e.UserFirstName = UnitInformation.UserFirstName; e.UserSecondName = UnitInformation.UserSecondName; e.Status = Status.No; e.OpenId = null; });
                    //    //result.IsSuccess = false;
                    //    //result.ErrorMsg = "该员工号已存在！";
                    //}
                }
                else//机组号不存在，直接插入
                {
                    result = InsertEntity(UnitInformation);
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
        /// 根据员工号和工厂名获取实体
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <param name="FactoryName"></param>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public ViewUnitInformation GetUserNumBig(string UnitNumber, string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            //string another = "StringVaule = " + UserNumber;
            string another = UnitNumber;
            if (!string.IsNullOrEmpty(UnitNumber))
                return Get(o => o.UnitNumber == another && o.WeixinPlatId == platId);
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
        public void ExportUserinfoInit(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<UnitInformation, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewUnitInformation> list = GetUsers(whereLambda);

            DataTable table = new DataTable();
            //List<string> headerTitles = new List<string> { "机组名称", "机组编号", "姓", "性别", "公司", "职务", "注册时间", "DBS代码", "状态", "生日", "地址", "固话", "审核时间" };
            List<string> headerTitles = new List<string> { "机组名称", "机组编号", "规格", "机组生产商", "机组类型", "生产时间", "安装时间", "机组型号", "经销商代码", "保质代码","保质到期日" };
            List<string> exportFields = new List<string> { "UnitName", "UnitNumber", "Specifications", "UnitProducer", "UnitType", "ProduceTime", "InstallTime", "UnitModel", "DealerNumber", "QualityGuaranteePeriod", "DeadLine" };
            table.Columns.Add("UnitName", typeof(string));
            table.Columns.Add("UnitNumber", typeof(string));
            table.Columns.Add("Specifications", typeof(string));
            table.Columns.Add("UnitProducer", typeof(string));
            table.Columns.Add("UnitType", typeof(string));
            table.Columns.Add("ProduceTime", typeof(DateTime));
            table.Columns.Add("InstallTime", typeof(DateTime));
            table.Columns.Add("UnitModel", typeof(string));
            table.Columns.Add("DealerNumber", typeof(string));
            table.Columns.Add("QualityGuaranteePeriod", typeof(string));
            table.Columns.Add("DeadLine", typeof(DateTime));
            //table.Columns.Add("RgisterTime", typeof(DateTime));
            if (list.Count > 0)
            {
                foreach (var user in list)
                {
                    DataRow row = table.NewRow();
                    row["UnitName"] = user.UnitName;
                    row["UnitNumber"] = user.UnitNumber;
                    row["Specifications"] = user.Specifications;
                    row["UnitProducer"] = user.UnitProducer;
                    row["UnitType"] = user.UnitType;
                    row["ProduceTime"] = user.ProduceTime;
                    row["InstallTime"] = user.InstallTime;
                    row["UnitModel"] = user.UnitModel;
                    row["DealerNumber"] = user.DealerNumber;
                    row["QualityGuaranteePeriod"] = user.QualityGuaranteePeriod;
                    row["DeadLine"] = user.DeadLine;
                    //row["Surname"] = user.Surname;
                    //row["Sex"] = user.Sex == 0 ? "男" : "女";
                    //row["Company"] = user.Company;
                    //row["Duty"] = user.Duty;
                    //row["RgisterTime"] = user.RgisterTime;
                    //row["DbsCode"] = user.DbsCode;
                    //row["UserType"] = user.UserType == 0 ? "普通用户" : "VIP用户";
                    //row["Birthday"] = user.Birthday;
                    //row["Address"] = user.Address;
                    //row["Phone"] = user.Phone;
                    //row["AditTime"] = user.AditTime.ToString();
                    table.Rows.Add(row);
                }
            }

            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        #region EPR导出
        public void ExportUserinfoInitERP(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<UnitInformation, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewUnitInformation> list = GetUsers(whereLambda);

            DataTable table = new DataTable();
            //List<string> headerTitles = new List<string> { "机组名称", "机组编号", "姓", "性别", "公司", "职务", "注册时间", "DBS代码", "状态", "生日", "地址", "固话", "审核时间" };
            List<string> headerTitles = new List<string> { "机组名称", "机组编号", "规格", "机组生产商", "机组类型", "生产时间" };
            List<string> exportFields = new List<string> { "UnitName", "UnitNumber", "Specifications", "UnitProducer", "UnitType", "ProduceTime" };
            table.Columns.Add("UnitName", typeof(string));
            table.Columns.Add("UnitNumber", typeof(string));
            table.Columns.Add("Specifications", typeof(string));
            table.Columns.Add("UnitProducer", typeof(string));
            table.Columns.Add("UnitType", typeof(string));
            table.Columns.Add("ProduceTime", typeof(DateTime));
            if (list.Count > 0)
            {
                foreach (var user in list)
                {
                    DataRow row = table.NewRow();
                    row["UnitName"] = user.UnitName;
                    row["UnitNumber"] = user.UnitNumber;
                    row["Specifications"] = user.Specifications;
                    row["UnitProducer"] = user.UnitProducer;
                    row["UnitType"] = user.UnitType;
                    row["ProduceTime"] = user.ProduceTime;
                    table.Rows.Add(row);
                }
            }

            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        public IList<ViewUnitInformation> GetUsers(Expression<Func<UnitInformation, bool>> whereLambda)
        {
            return GetMany(whereLambda);
        }

    }


}
