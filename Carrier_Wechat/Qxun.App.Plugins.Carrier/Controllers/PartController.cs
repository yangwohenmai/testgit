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
    [ActionAuthority("模块管理", "零件信息管理", "Part")]
    public class PartController : WeixinPlatBaseController
    {

        #region  零件信息列表

        [Menu("functionBigTitle", "零件信息管理", 17, true, "~/Content/Images/PluginImages/B_Community.png", "添加一个零件信息。")]
        public ActionResult PartInformation()
        {         

            ViewBag.TotalCount = new PartInformationBll().GetPartInformationTotalCount(CurrentWeixinPlat.WeixinPlatId);
            return View();
        }
        #endregion


        #region 获取零件信息表
        /// <summary>
        /// 获取零件信息列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="searchJson"></param>
        /// <returns></returns>

        public QxunJsonResult GetPartInformationPageList(int pageSize, int page, string searchJson)
        {
            string platId = CurrentWeixinPlat.WeixinPlatId;
            //将搜索条件的json数据传递给GetWhereLambda方法，返回拼接完成的搜索条件
            var lambda = GetWhereLambda(searchJson);
            var result = new PartInformationBll().GetPartInformationListPageList(pageSize, page, CurrentWeixinPlat.WeixinPlatId, lambda, e => e.PartId, true);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 拼接whereLambda搜索条件
        /// </summary>
        /// <param name="searchJson"></param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<PartInformation, bool>> GetWhereLambda(string searchJson)
        {
            var whereLambda = PredicateBuilder.True<PartInformation>();
            if (!string.IsNullOrEmpty(searchJson))
            {
                PartInfomationSearchEntity model = JsonHelper.DeserializeObject<PartInfomationSearchEntity>(searchJson);
                if (!string.IsNullOrEmpty(model.PartNumber))
                {
                    whereLambda = whereLambda.And(e => e.PartNumber.Contains(model.PartNumber));
                }
            }
            return whereLambda;
        }

        #endregion

        #region 编辑零件信息
        /// <summary>
        /// 显示编辑零件信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPartInfo()
        {
            return View();
        }

        /// <summary>
        /// 添加或修改零件信息
        /// </summary>
        /// <param name="json">零件信息数据</param>
        /// <returns>返回添加或修改结果</returns>

        [ValidateInput(false)]
        public QxunJsonResult InsertOrUpdatePartInformationEntity(string json)
        {
            var result = new PartInformationBll().InsertOrUpdateEntity(json);
            return new QxunJsonResult(result);
        }
        /// <summary>
        ///自动获取零件已有信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QxunJsonResult GetPartInformationEntityById(string id)
        {
            var result = new PartInformationBll().GetEntityById(id);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 批量导入
        public QxunJsonResult ImportPartListByFile(string vipType)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            HttpPostedFileBase file = Request.Files["file1"];//从页面前台获取文件路径
            if (file == null)
            {
                result.ErrorMsg = "未上传文件，请选择批量导入的CVS数据！";
                result.IsSuccess = false;
                return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
            }
            result = new PartInformationBll().BatImportFromFile(file, CurrentWeixinPlat);//第一个参数，文件路径
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
            var result = new RecordInfoBll().GetRecord(CurrentWeixinPlat.WeixinPlatId, RecordType.Part);
            return new QxunJsonResult(result);
        }
        #endregion 

        #region 批量导出
        public ActionResult ExportPartinfoToExcel(string PartNumber)
        {
            var where = GetPartInfoWhereLambard(PartNumber);

            string excelName = "零件信息";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new PartInformationBll().ExportPartinfoInit(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }
        #endregion

        #region ERP导出
        public ActionResult ExportPartinfoToExcelERP(string PartNumber)
        {
            var where = GetPartInfoWhereLambard(PartNumber);

            string excelName = "零件信息ERP";
            string[] headers;
            string[] keyFields;
            IQueryable rows;
            new PartInformationBll().ExportPartinfoInitERP(out headers, out keyFields, out rows, where);
            IWorkbook workbook = ExcelUtil.ExportWorkbook(excelName, headers, rows, keyFields);
            new Qxun.App.Plugins.CarrierCore.Utils.Excel.ExcelResult(excelName, workbook).ExecuteResult(ControllerContext);
            return View();
        }
        #endregion


        private Expression<Func<PartInformation, bool>> GetPartInfoWhereLambard(string PartNumber)
        {
            Expression<Func<PartInformation, bool>> whereLambda = PredicateBuilder.True<PartInformation>();
            if (!string.IsNullOrEmpty(PartNumber))
            {
                whereLambda = whereLambda.And(e => e.PartNumber.Contains(PartNumber.Trim()));
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
