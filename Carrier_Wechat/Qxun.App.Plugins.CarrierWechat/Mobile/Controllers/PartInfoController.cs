using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.Core.Common;
using System.Web.Mvc;

namespace Qxun.App.Plugins.CarrierWechat.Mobile.Controllers
{
    public class PartInfoController : BaseController
    {
        /// <summary>
        /// 零件搜索页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public ActionResult PartInfo(string u, string r, string a)
        {
            return View();
        }
        /// <summary>
        /// 根据零件编号查询评论列表
        /// </summary>
        /// <param name="u">微信用户标识</param>
        /// <param name="r">平台编号</param>
        /// <param name="a">用户编号</param>
        /// <param name="articleId">文章编号</param>
        /// <returns></returns>
        public QxunJsonResult GetPartInformationList(string u, string r, string a, string PartNumber)
        {
            var result = new PartInformationBll().GetPartInfo(r, PartNumber);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckResult(string u, string r, string a) 
        {
            return View();
        }


    }
}
