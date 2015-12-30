#if DEBUG
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.App.Weistone.Mobile.Public;
using Qxun.App.Weistone.Public;
using Qxun.Core.Common;
using Qxun.Plugs.MicMembership.Services;
using Qxun.Plugs.WeiEvent.Public;
using Qxun.Plugs.WeiEvent.Services;
using Qxun.Plugs.WeiEvent.ViewModels;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qxun.Plugs.WeiEvent.Mobile.Controllers
{
    public class NewWeiActiveController : WeiActiveController
    {
        /// <summary>
        /// 大转盘
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiActiveLottery(string u, string r, string a, string id)
        {
            return base.WeiActiveLottery(u, r, a, id);
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiActiveCoupon(string u, string r, string a, string id)
        {
            return base.WeiActiveCoupon(u, r, a, id);

        }

        /// <summary>
        /// 刮刮卡
        /// </summary> 
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns> 
        public override ActionResult WeiActiveScratch(string u, string r, string a, string id)
        {
            return base.WeiActiveScratch(u, r, a, id);

        }

        /// <summary>
        /// 砸金蛋
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult WeiActiveHitGoldenEgg(string u, string r, string a, string id)
        {
            return base.WeiActiveHitGoldenEgg(u, r, a, id);

        }
    }
}
#endif
