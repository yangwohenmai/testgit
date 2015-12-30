using Qxun.App.Plugins.CarrierWechat.Public;
using Qxun.App.Plugins.CarrierWechat.Services;
using Qxun.App.Plugins.CarrierWechat.ViewModels;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.Core.Common;
using Qxun.Plugs.MicMembership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.CarrierWechat.Mobile.Controllers
{
    /// <summary>
    /// 留言手机端
    /// </summary>
    public class MessageBoardMobileController : BaseController
    {
        #region 手机端留言列表页面
        /// <summary>
        /// 手机端留言列表页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public ActionResult MessageBoardMobileList(string u, string r, string a)
        {
            return View();
        }
        #endregion

        #region 我要留言编辑页面
        /// <summary>
        /// 我要留言编辑页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public ActionResult MessageBoardEdit(string u, string r, string a)
        {
            //编辑页面需要初始化加载姓名和手机号
            //var result = new MembershipCardBll().GetMembershipCardByWeixinUserId(r, u, a);
            //if(result!=null){
            //    ViewBag.CustomerName=result.;
            //    ViewBag.TelPhone=;
            //}
            return View();
        } 
        #endregion

        #region 留言详情页面
        /// <summary>
        /// 留言详情页面
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MessageBoardInfo(string u, string r, string a)
        {
            return View();
        } 
        #endregion

        #region 根据id获取留言详情
        /// <summary>
        /// 根据id获取留言详情
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public QxunJsonResult GetMessageBoardEntityById(string u, string r, string a, string id)
        {
            var result = new MessageBoardBll().GetEntityById(id);
            return new QxunJsonResult(result);
        } 
        #endregion

        #region 获取手机端留言列表（openId）
        /// <summary>
        /// 获取手机端留言列表（openId）
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public QxunJsonResult GetMessageBoardMobileEntities(string u, string r, string a, int page, int pageSize)
        {
            var result = new MessageBoardBll().GetEntitiesByFId(page, pageSize, r, e => e.OpenId == u, e => e.MessageDate, false);
            return new QxunJsonResult(result);
        } 
        #endregion

        #region 更新或编辑留言
        /// <summary>
        /// 更新或编辑留言
        /// </summary>
        /// <param name="u"></param>
        /// <param name="r"></param>
        /// <param name="a"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QxunJsonResult InsertOrUpdateMessageBoardEntity(string u, string r, string a, ViewMessageBoard entity)
        {
            SuccessResponseResult result = new SuccessResponseResult();
            if (entity != null)
            {
                entity.MessageDate = DateTime.Now;
                entity.MessageStatus = MessageStatus.Unreply;
                entity.OpenId = u;
                entity.WeixinPlatId = CurrentWeixinPlat.WeixinPlatId;
                result = new MessageBoardBll().InsertOrUpdateEntity(entity);
            }
            else {
                result.IsSuccess = false;
                result.ErrorMsg = "插入失败";
            }
            return new QxunJsonResult(result);
        } 
        #endregion
    }
}
