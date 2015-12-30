using CarrierCore.Entities;
using CarrierCore.Models;
using CarrierCore.Services;
using NPOI.SS.UserModel;
using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.App.Plugins.CarrierCore.Utils;
using Qxun.App.Weistone.Controllers;
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
    [ActionAuthority("模块管理", "经销商管理", "Dealer")]
    public class DealerController : WeixinPlatBaseController
    {
        [Menu("functionBigTitle", "经销商管理", 19, true, "~/Content/Images/PluginImages/B_Community.png", "添加一个经销商管理。")]
        #region 经销商信息表
        public ActionResult DealerInfo()
        {
            ViewBag.TotalCount = new DealerBll().GetDealerTotalCount(CurrentWeixinPlat.WeixinPlatId);

           // ViewBag.TotalCount = new PartInformationBll().GetPartInformationTotalCount(CurrentWeixinPlat.WeixinPlatId);
            return View();
        }
        #endregion

        #region 获取经销商信息列表
        /// <summary>
        /// 获取经销商信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchJson"></param>
        /// <returns></returns>
        public QxunJsonResult GetDealerPageList(int pageSize, int page, string searchJson)
        {
            string platId = CurrentWeixinPlat.WeixinPlatId;
            //将搜索条件的json数据传递给GetWhereLambda方法，返回拼接完成的搜索条件
            var lambda = GetWhereLambda(searchJson);
            var result = new DealerBll().GetDealerListPageList(pageSize, page, CurrentWeixinPlat.WeixinPlatId, lambda, e => e.DealerId, true);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        public static System.Linq.Expressions.Expression<Func<Dealer, bool>> GetWhereLambda(string searchJson)
        {
            var whereLambda = PredicateBuilder.True<Dealer>();
            if (!string.IsNullOrEmpty(searchJson))
            {
                DealerSearchEntity model = JsonHelper.DeserializeObject<DealerSearchEntity>(searchJson);
                if (!string.IsNullOrEmpty(model.SiteName))
                {
                    whereLambda = whereLambda.And(e => e.SiteName.Contains(model.SiteName));
                }
                if (!string.IsNullOrEmpty(model.CompanyName))
                {
                    whereLambda = whereLambda.And(e => e.CompanyName.Contains(model.CompanyName));
                }
            }
            return whereLambda;
        }
        #endregion

        #region 批量导入
        public QxunJsonResult ImportDealerListByFile(string vipType)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            HttpPostedFileBase file = Request.Files["file1"];//从页面前台获取文件路径
            if (file == null)
            {
                result.ErrorMsg = "未上传文件，请选择批量导入的CVS数据！";
                result.IsSuccess = false;
                return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
            }
            result = new DealerBll().BatImportFromFile(file, CurrentWeixinPlat);//第一个参数，文件路径
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region 记录错误
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <returns></returns>
        public QxunJsonResult Record()
        {
            var result = new RecordInfoBll().GetRecord(CurrentWeixinPlat.WeixinPlatId, RecordType.Dealer);
            return new QxunJsonResult(result);
        }
        #endregion

        #region 批量导出
        public ActionResult ExportDealerinfoToExcel(string SiteName, string CompanyName)
        {
            var where = GetDealerInfoWhereLambard(SiteName, CompanyName);

            string excelName = "授权经销商信息";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new DealerBll().ExportDealerinfoInit(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }
        #endregion

        #region ERP导出
        public ActionResult ExportDealerinfoToExcelERP(string SiteName, string CompanyName)
        {
            var where = GetDealerInfoWhereLambard(SiteName, CompanyName);

            string excelName = "授权经销商信息ERP";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new DealerBll().ExportUserinfoInitERP(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }
        #endregion


        private Expression<Func<Dealer, bool>> GetDealerInfoWhereLambard(string SiteName, string CompanyName)
        {
            Expression<Func<Dealer, bool>> whereLambda = PredicateBuilder.True<Dealer>();
            if (!string.IsNullOrEmpty(SiteName))
            {
                whereLambda = whereLambda.And(e => e.SiteName.Contains(SiteName.Trim()));
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                whereLambda = whereLambda.And(e => e.CompanyName.Contains(CompanyName.Trim()));
            }
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
