using CarrierCore.Models;
using CarrierCore.Services;
using NPOI.SS.UserModel;
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
using System.Web.Mvc;

namespace Qxun.App.Plugins.Carrier.Controllers
{
    [ActionAuthority("模块管理", "400热线管理", "HotLine")]
    public class HotLineController : WeixinPlatBaseController
    {
        #region  400热线管理信息列表

        [Menu("functionBigTitle", "400热线管理", 208, true, "~/Content/Images/PluginImages/B_Community.png", "添加400热线管理。")]
        public ActionResult HotLineInfo()
        {

            return View();
        }
        #endregion

        #region  新增呼叫单
        public ActionResult AddInfo()
        {
            //var list = new ArticleCategoryBll().GetArticleCategoryList(CurrentWeixinPlat.WeixinPlatId);
            //if (list != null && list.Count > 0)
            //{
            //    Dictionary<string, string> articleCategoryDic = new ArticleCategoryBll().GetArticleCategoryName(list.Select(e => e.ArticleCategoryId).ToList());
            //    ViewBag.articleCategoryList = articleCategoryDic;
            //}
            return View();
        }
        [ValidateInput(false)]
        public QxunJsonResult InsertOrUpdateHotLineEntity(string json)
        {
            var result = new HotLineBll().InsertOrUpdateEntity(json);
            return new QxunJsonResult(result);
        }
        #endregion

        #region  导出Excel
        public ActionResult ExportHotLineInfoToExcel(string TicketNumber)
        {
            var where = GetUnitInfoWhereLambard(TicketNumber);

            string excelName = "呼叫单列表";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new HotLineBll().ExportHotLineInfoInit(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }

        private Expression<Func<HotLine, bool>> GetUnitInfoWhereLambard(string TicketNumber)
        {
            Expression<Func<HotLine, bool>> whereLambda = PredicateBuilder.True<HotLine>();
            if (!string.IsNullOrEmpty(TicketNumber))
            {
                whereLambda = whereLambda.And(e => e.TicketNumber.Contains(TicketNumber.Trim()));
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
        #endregion


        #region  修改呼叫单
        public ActionResult EditHotLine()
        {
            //var list = new ArticleCategoryBll().GetArticleCategoryList(CurrentWeixinPlat.WeixinPlatId);
            //if (list != null && list.Count > 0)
            //{
            //    Dictionary<string, string> articleCategoryDic = new ArticleCategoryBll().GetArticleCategoryName(list.Select(e => e.ArticleCategoryId).ToList());
            //    ViewBag.articleCategoryList = articleCategoryDic;
            //}
            return View();
        }
        [ValidateInput(false)]
        public QxunJsonResult InsertOrUpdatePartInformationEntity(string json)
        {
            var result = new HotLineBll().InsertOrUpdateEntity(json);
            return new QxunJsonResult(result);
        }
        #endregion

        #region  显示判断
        //public QxunJsonResult Show()
        //{
        //    var result = new RecordInfoBll().GetRecord(CurrentWeixinPlat.WeixinPlatId);

        //    //string qq = "1";
        //    //var LastUploadTime = result.UploadTime;
        //    //var SuccessRecord = result.SuccessRecord;
        //    //var FailRecord = result.FailRecord;
        //    //var AllRecord = result.AllUploadRecord;

        //    return new QxunJsonResult(result); 
        //}
        #endregion
    }
}
