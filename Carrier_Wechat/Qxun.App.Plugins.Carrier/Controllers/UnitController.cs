using NPOI.SS.UserModel;
using Qxun.App.Plugins.CarrierCore.Entities;
using Qxun.App.Plugins.CarrierCore.Models;
using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.App.Plugins.CarrierCore.Utils;
using Qxun.App.Weistone.Controllers;
using Qxun.App.Weistone.Public;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Qxun.App.Plugins.Carrier.Controllers
{
    [ActionAuthority("模块管理", "机组信息管理", "Unit")]
    public class UnitController : WeixinPlatBaseController
    {
        [Menu("functionBigTitle", "机组信息管理", 16, true, "~/Content/Images/PluginImages/B_Community.png", "添加一个机组信息管理。")]
        public ActionResult UnitInformation()
        {
            //ViewBag.preview = UrlBuilder.CreateMobileUrl("/Register/Register", CurrentWeixinPlat.WeixinPlatId, "Preview");
            //ViewBag.CopyUrl = UrlBuilder.CreateMobileUrl("/Register/Register", CurrentWeixinPlat.WeixinPlatId, Guid.NewGuid().ToString());

            ViewBag.TotalCount = new UnitInformationBll().GetUnitInformationTotalCount(CurrentWeixinPlat.WeixinPlatId);
            return View();
        }

        #region 获取机组信息列表
        /// <summary>
        /// 获取社区用户列表
        /// </summary>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="page">获取第几页数据</param>
        /// <param name="searchJson">搜索条件</param>
        /// <returns>返回查询到的社区用户列表数据</returns>
        public QxunJsonResult GetUnitInformationPageList(int pageSize, int page, string searchJson)
        {
            string platId = CurrentWeixinPlat.WeixinPlatId;
            //将搜索条件的json数据传递给GetWhereLambda方法，返回拼接完成的搜索条件
            var lambda = GetWhereLambda(searchJson);
            var result = new UnitInformationBll().GetEntitiesByFId(page, pageSize, platId, lambda, e => e.UnitId, false);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 拼接WhereLambda搜索条件
        /// </summary>
        /// <param name="searchJson">搜索条件</param>
        /// <returns>返回拼接完成的WhereLambda搜索条件</returns>
        private static System.Linq.Expressions.Expression<Func<UnitInformation, bool>> GetWhereLambda(string searchJson)
        {
            var whereLambda = PredicateBuilder.True<UnitInformation>();
            if (!string.IsNullOrEmpty(searchJson))
            {
                UnitInformationSearchEntity model = JsonHelper.DeserializeObject<UnitInformationSearchEntity>(searchJson);
                if (!string.IsNullOrEmpty(model.UnitName.Trim()))
                {
                    whereLambda = whereLambda.And(e => e.UnitName.Contains(model.UnitName.Trim()));
                }
                //if (!string.IsNullOrEmpty(model.userFir.Trim()))
                //{
                //    whereLambda = whereLambda.And(e => e.UserFirstName.Contains(model.userFir.Trim()));
                //}
                //if (!string.IsNullOrEmpty(model.userSec.Trim()))
                //{
                //    whereLambda = whereLambda.And(e => e.UserSecondName.Contains(model.userSec.Trim()));
                //}
                //if (model.Status.HasFlag(Status.Yes) || model.Status.HasFlag(Status.No) && !model.Status.HasFlag(Status.All))
                //{
                //    whereLambda = whereLambda.And(e => e.Status == model.Status);
                //}
                //if (!string.IsNullOrEmpty(model.UserFac.Trim()))//这里的Fac取自UserListSearchEntity，并且要和前台的<input id="">相对应
                //{
                //    whereLambda = whereLambda.And(e => e.FactoryName.Contains(model.UserFac.Trim()));
                //}
            }
            return whereLambda;
        }
        #endregion

        #region 批量导入
        public QxunJsonResult ImportUnitListByFile(string vipType)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            HttpPostedFileBase file = Request.Files["file1"];//从页面前台获取文件路径
            if (file == null)
            {
                result.ErrorMsg = "未上传文件，请选择批量导入的CVS数据！";
                result.IsSuccess = false;
                return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
            }
            result = new UnitInformationBll().BatImportFromFile(file, CurrentWeixinPlat);//第一个参数，文件路径
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <returns></returns>
        public QxunJsonResult Record()
        {
            var result = new RecordInfoBll().GetRecord(CurrentWeixinPlat.WeixinPlatId, RecordType.Unit);

            //string qq = "1";
            //var LastUploadTime = result.UploadTime;
            //var SuccessRecord = result.SuccessRecord;
            //var FailRecord = result.FailRecord;
            //var AllRecord = result.AllUploadRecord;

            return new QxunJsonResult(result);
        }

        public ActionResult ExportUnitinfoToExcel(string UnitName)
        {
            var where = GetUnitInfoWhereLambard(UnitName);

            string excelName = "机组信息";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new UnitInformationBll().ExportUserinfoInit(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }

        public ActionResult ExportUnitinfoToExcelERP(string UnitName)
        {
            var where = GetUnitInfoWhereLambard(UnitName);

            string excelName = "机组信息ERP";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new UnitInformationBll().ExportUserinfoInitERP(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }

        private Expression<Func<UnitInformation, bool>> GetUnitInfoWhereLambard(string UnitName)
        {
            Expression<Func<UnitInformation, bool>> whereLambda = PredicateBuilder.True<UnitInformation>();
            if (!string.IsNullOrEmpty(UnitName))
            {
                whereLambda = whereLambda.And(e => e.UnitName.Contains(UnitName.Trim()));
            }
            //if (!string.IsNullOrEmpty(txtName))
            //{
            //    whereLambda = whereLambda.And(e => e.MobileNum.Contains(txtName.Trim()));
            //}
            //if (dateStart != null)
            //{
            //    whereLambda = whereLambda.And(e => e.RgisterTime >= dateStart);
            //}
            //if (dateEnd != null)
            //{
            //    whereLambda = whereLambda.And(e => e.RgisterTime <= dateEnd);
            //}
            return whereLambda;
        }
    }
}
