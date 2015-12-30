using Qxun.App.Plugins.CarrierWechat.Public;
using Qxun.App.Plugins.CarrierWechat.Services;
using Qxun.App.Weistone.Controllers;
using Qxun.App.Weistone.Public;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.CarrierWechat.Controllers
{
    [ActionAuthority("模块管理", "留言管理", "MessageBoard")]
    public class MessageBoardController:WeixinPlatBaseController
    {
        [Menu("functionBigTitle", "留言管理", 19, true, "~/Content/Images/PluginImages/B_Community.png", "添加一个留言管理。")]

        #region 留言管理列表页面
        /// <summary>
        /// 留言管理列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageBoardList()
        {
            ViewBag.preview = UrlBuilder.CreateMobileUrl("/MessageBoardMobile/MessageBoardMobileList", CurrentWeixinPlat.WeixinPlatId, "Preview");
            ViewBag.CopyUrl = UrlBuilder.CreateMobileUrl("/MessageBoardMobile/MessageBoardMobileList", CurrentWeixinPlat.WeixinPlatId, Guid.NewGuid().ToString());
            return View();
        }
        #endregion

        #region 留言详情
        /// <summary>
        /// 留言详情
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageBoardInfo()
        {
            var result = new MessageBoardBll().GetEntityById(Request["id"]);
            var entity = result.ExtInfo as ViewModels.ViewMessageBoard;
            return View(entity);
        } 
        #endregion

        #region 获取留言列表
        /// <summary>
        /// 获取留言列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchJson"></param>
        /// <returns></returns>
        public QxunJsonResult GetMessageBoardEntities(int page, int pageSize, string messageTime,string messageContent)
        {
            //1、得到开始时间和结束时间
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrWhiteSpace(messageTime))
            {
                string[] times = messageTime.Split('-');
                DateTime temp;
                if (DateTime.TryParse(times[0] + "-" + times[1] + "-" + times[2], out temp))
                {
                    startDate = temp;
                }
                if (DateTime.TryParse(times[3] + "-" + times[4] + "-" + times[5], out temp))
                {
                    endDate = temp.AddDays(1);
                }
            }
            //2、拼接lambda表达式
            var lambda = GetWhereLambda(startDate, endDate, messageContent);
            //3、查询
            var result = new MessageBoardBll().GetEntitiesByFId(page, pageSize, CurrentWeixinPlat.WeixinPlatId, lambda, e => e.Id, false);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 拼接WhereLambda搜索条件
        /// </summary>
        /// <param name="searchJson">搜索条件</param>
        /// <returns>返回拼接完成的WhereLambda搜索条件</returns>
        private static System.Linq.Expressions.Expression<Func<Models.MessageBoard, bool>> GetWhereLambda(DateTime? dateStart, DateTime? dateEnd, string content)
        {
            var whereLambda = PredicateBuilder.True<Models.MessageBoard>();
            if (dateStart != null)
            {
                whereLambda = whereLambda.And(e => e.MessageDate >= dateStart);
            }
            if (dateEnd != null)
            {
                whereLambda = whereLambda.And(e => e.MessageDate <= dateEnd);
            }
            if (!string.IsNullOrEmpty(content))
            {
                whereLambda = whereLambda.And(e => e.MessageContent.Contains(content));
            }
            return whereLambda;
        } 
        #endregion

        #region 更新回复内容
        /// <summary>
        /// 更新回复内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="replyContent"></param>
        /// <returns></returns>
        public QxunJsonResult UpdateMessageBoardEntity(string id, string replyContent)
        {
            var result = new MessageBoardBll().UpdateEntity(id, e => { e.ReplyContent = replyContent; e.MessageStatus = MessageStatus.Replied; });
            return new QxunJsonResult(result);
        } 
        #endregion
    }
}
