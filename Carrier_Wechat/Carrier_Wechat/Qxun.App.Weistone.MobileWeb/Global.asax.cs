using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Qxun.App.Weistone.MobileWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public override void Init()
        {
            //跨进程共享Session
            var sessionStateStoreProvider = typeof(HttpSessionState).Assembly.GetType("System.Web.SessionState.OutOfProcSessionStateStore");
            var uriBaseField = sessionStateStoreProvider.GetField("s_uribase", BindingFlags.Static | BindingFlags.NonPublic);
            uriBaseField.SetValue(null, FormsAuthentication.CookieDomain);
            base.Init();
        }

        protected void Application_Start()
        {
            Qxun.Core.Global.RegisterAllPluginsAssembly();

            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new QxunMobileViewEngine());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}