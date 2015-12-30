#if DEBUG
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Security.Policy;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.Plugs.MicWebsite.Public;
using Qxun.Util;
using Qxun.Plugs.MicWebsite.Entities;
using Qxun.Plugs.MicWebsite.ViewModels;
using Qxun.Plugs.MicWebsite.Services;
using Qxun.Plugs.MicMembership.Services;
using Qxun.Plugs.CMS.ViewModels;
using Qxun.Plugs.CMS.Services;
using Qxun.App.Weistone.ViewModels;
using Qxun.App.Weistone.Services;
using UrlHelper = System.Web.Mvc.UrlHelper;
using Qxun.Core;
using Qxun.App.Weistone.Public;
using Qxun.App.Weistone.Mobile.Public;
using Qxun.App.Plugins.Analytics.Attributes;

namespace Qxun.Plugs.MicWebsite.Mobile.Controllers
{
    public class NewWeiSiteTemplateController : WeiSiteTemplateController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public override ActionResult Home(string u, string r, string a)
        {
            return base.Home(u, r, a);
        }

        public override ActionResult More(string u, string r, string a, string moreid)
        {
            return base.More(u, r, a, moreid);
        }

        /// <summary>
        /// 频道
        /// </summary>
        /// <returns></returns>
        public override ActionResult Channel(string u, string r, string a, string pid)
        {
            return base.More(u, r, a, pid);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public override ActionResult Article(string u, string r, string a, string ids)
        {
            return base.More(u, r, a, ids);
        }

        /// <summary>
        /// 内容页
        /// </summary>
        /// <returns></returns>
        [CounterAttribute(Order = 99)]
        [ValidateInput(false)]
        public override ActionResult Details(string u, string r, string a, string id)
        {
            return base.Details(u, r, a, id);
        }
    }
}
#endif
