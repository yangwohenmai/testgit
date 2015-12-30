using CarrierCore.Entities;
using CarrierCore.Models;
using CarrierCore.Services;
using Qxun.App.Weistone.Controllers;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.Carrier.Controllers
{
    [ActionAuthority("模块管理", "维修项工时管理", "Repair")]
    public class RepairController : WeixinPlatBaseController
    {
        #region  维修项工时管理信息列表

        [Menu("functionBigTitle", " 维修项工时管理",207, true, "~/Content/Images/PluginImages/B_Community.png", "添加维修工时管理信息。")]
        public ActionResult RepairInfo()
        {

            return View();
        }
        #endregion

        #region 获取工时管理信息表
        /// <summary>
        /// 获取工时管理信息表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="searchJson"></param>
        /// <returns></returns>

        public QxunJsonResult GetRepairPageList(int pageSize, int page, string searchJson)
        {
            string platId = CurrentWeixinPlat.WeixinPlatId;
            //将搜索条件的json数据传递给GetWhereLambda方法，返回拼接完成的搜索条件
            var lambda = GetWhereLambda(searchJson);
            var result = new RepairBll().GetRepairListPageList(pageSize, page, CurrentWeixinPlat.WeixinPlatId, lambda, e => e.RepairId, true);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 拼接whereLambda搜索条件
        /// </summary>
        /// <param name="searchJson"></param>
        /// <returns></returns>
        public static System.Linq.Expressions.Expression<Func<Repair, bool>> GetWhereLambda(string searchJson)
        {
            var whereLambda = PredicateBuilder.True<Repair>();
            if (!string.IsNullOrEmpty(searchJson))
            {
                RepairSearchEntity model = JsonHelper.DeserializeObject<RepairSearchEntity>(searchJson);
                if (!string.IsNullOrEmpty(model.RepairName))
                {
                    whereLambda = whereLambda.And(e => e.RepairName.Contains(model.RepairName));
                }
            }
            return whereLambda;
        }

        #endregion
    }
}
