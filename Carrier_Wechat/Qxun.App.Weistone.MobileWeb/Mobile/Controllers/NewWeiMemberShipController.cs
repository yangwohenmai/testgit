#if DEBUG
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.App.Weistone.Mobile.Public;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Plugs.MicMembership.Entities;
using Qxun.Plugs.MicMembership.Public;
using Qxun.Plugs.MicMembership.Services;
using Qxun.Plugs.MicMembership.ViewModels;
using Qxun.Plugs.WeiStore.Services;
using Qxun.Plugs.WeiStore.ViewModels;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qxun.Plugs.MicWebsite.Mobile.Controllers
{
    public class NewWeiMemberShipController : WeiMemberShipController
    {
        /// <summary>
        /// 会员卡首页
        /// </summary>
        /// <returns></returns>
        public override ActionResult WeiMemberShip(string u, string r, string a)
        {
            return base.WeiMemberShip(u, r, a);
        }

        /// <summary>
        /// 领取会员卡
        /// </summary>
        public override ActionResult GetWeiMemberShipPage(string u, string r, string a)
        {
            return base.GetWeiMemberShipPage(u, r, a);
        }

        /// <summary>
        /// 通知列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiMemberShipNoticeList(string u, string r, string a)
        {
            return base.WeiMemberShipNoticeList(u, r, a);
        }

        /// <summary>
        /// 特权列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiMemberShipPrivilegeList(string u, string r, string a)
        {
            return base.WeiMemberShipPrivilegeList(u, r, a);
        }

        /// <summary>
        /// 会员卡说明列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiMemberShipCardNoteList(string u, string r, string a)
        {
            return base.WeiMemberShipCardNoteList(u, r, a);
        }

        /// <summary>
        /// 个人资料
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult WeiMemberShipUserDetail(string u, string r, string a, string id)
        {
            return base.WeiMemberShipUserDetail(u, r, a, id);
        }

        /// <summary>
        /// 消费记录
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <param name="month"> </param>
        /// <returns></returns>
        public override ActionResult WeiMembershipConsumeRecord(string u, string r, string a, string id, string month)
        {
            return base.WeiMembershipConsumeRecord(u, r, a, id, month);
        }

        /// <summary>
        /// 积分记录
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <param name="month"> </param>
        /// <returns></returns>
        public override ActionResult WeiMembershipIntegralRecord(string u, string r, string a, string id, string month)
        {
            return base.WeiMembershipIntegralRecord(u, r, a, id, month);
        }

        /// <summary>
        /// 签到记录
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <param name="month"> </param>
        /// <returns></returns>
        public override ActionResult WeiMembershipSignInRecord(string u, string r, string a, string id, string month)
        {
            return base.WeiMembershipSignInRecord(u, r, a, id, month);
        }

        /// <summary>
        /// 我的礼品列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult WeiMembershipMyPresentList(string u, string r, string a, string id)
        {
            return base.WeiMembershipMyPresentList(u, r, a, id);
        }

        /// <summary>
        /// 积分换礼品列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult WeiMembershipPresentList(string u, string r, string a, string id)
        {
            return base.WeiMembershipPresentList(u, r, a, id);
        }

        /// <summary>
        /// 门店列表
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public override ActionResult WeiMembershipStoreList(string u, string r, string a)
        {
            return base.WeiMembershipStoreList(u, r, a);
        }

        /// <summary>
        /// 获取门店详细信息
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult WeiMembershipStore(string u, string r, string a, string id)
        {
            return base.WeiMembershipStore(u, r, a, id);
        }
    }
}
#endif
