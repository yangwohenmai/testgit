#if DEBUG
using Qxun.App.Plugins.WeiReservation.Public;
using Qxun.App.Plugins.WeiReservation.Services;
using Qxun.App.Plugins.WeiReservation.ViewModels;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.App.Weistone.Mobile.Public;
using Qxun.App.Weistone.Services;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Plugs.MicMembership.Services;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qxun.App.Plugins.WeiReservation.Mobile.Controllers
{
    public class NewWeiReserveController : WeiReserveController
    {
        /// <summary>
        /// 订单首页
        /// </summary>
        /// <returns></returns>
        public override ActionResult Reserve(string u, string r, string a, string id)
        {
            return base.Reserve(u, r, a, id);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public override ActionResult OrderList(string u, string r, string a, string id, string title)
        {
            return base.OrderList(u, r, a, id, title);
        }

        /// <summary>
        /// 投票成功后展示的列表
        /// </summary>
        /// <returns></returns>
        public override ActionResult VoteResultShow(string u, string r, string a, string id)
        {
            return base.VoteResultShow(u, r, a, id);
        }

        /// <summary>
        /// 调研次数结束，跳转的页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public override ActionResult ResearchResultShow(string u, string r, string a, string resourceId)
        {
            return base.ResearchResultShow(u, r, a, resourceId);
        }
    }
}
#endif