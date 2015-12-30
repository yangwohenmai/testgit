using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.CarrierWechat.Mobile.Controllers
{
    public class UnitInfoController : BaseController
    {
        /// <summary>
        /// 机组信息搜索页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public ActionResult UnitInfo(string u,string r,string a)
        {
            return View();
        }
        /// <summary>
        /// 获取机组信息列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public QxunJsonResult GetUnitInformationList(string u, string r, string a, string UnitNumber)
        {
            var result = new UnitInformationBll().GetUnitInfo(r, UnitNumber);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UnitCheckResult(string u,string r,string a)
        {
            return View();
        }
    }
}
