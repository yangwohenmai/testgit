using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qxun.App.Weistone.MobileWeb
{
    public class QxunMobileViewEngine : RazorViewEngine
    {
        public QxunMobileViewEngine()
        {
            //主视图路径 
            ViewLocationFormats = new[] { "~/Mobile/Views/{1}/{0}.cshtml", "~/Mobile/Views/Shared/{0}.cshtml" };
            //主模版路径 
            MasterLocationFormats = new[] { "~/Mobile/Views/Shared/{0}.cshtml" };
            //主分部视图路径 
            PartialViewLocationFormats = new[] { "~/Mobile/Views/{1}/{0}.cshtml", "~/Mobile/Views/Shared/{0}.cshtml" }; 
        }
    }
}