using Qxun.App.Weistone.Controllers;
using Qxun.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.Carrier.Controllers
{
    [ActionAuthority("模块管理", "保外工单", "WorkOrderOut")]
    public class WorkOrderOutController : BaseController
    {
        #region  400热线管理信息列表

        [Menu("functionBigTitle", "保外工单", 209, true, "~/Content/Images/PluginImages/B_Community.png", "添加保外工单。")]
        public ActionResult WorkOrderOut()
        {

            return View();
        }
        #endregion
    }
}
