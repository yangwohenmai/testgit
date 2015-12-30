using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.App.Plugins.KnowledgeBase.Services;
using Qxun.App.Plugins.KnowledgeBase.ViewModels;
using Qxun.App.Weistone.Controllers;
using Qxun.App.Weistone.Public;
using Qxun.App.Weistone.Services;
using Qxun.App.Weistone.ViewModels;
using Qxun.Core.Attributes;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxun.App.Plugins.KnowledgeBase.Controllers
{
     [ActionAuthority("模块管理", "知识库", "KnowledgeBase")]
    public class KnowledgeBaseController : WeixinPlatBaseController
    {
        [Menu("functionBigTitle", "知识库", 15, true, "~/Content/Images/PluginImages/B_KnowledgeBase.png", "产品知识库")]

        #region 知识库

        #region 知识库列表页面
            /// <summary>
            /// 知识库列表页面
            /// </summary>
            /// <returns></returns>
            public ActionResult KnowledgeList()
            {
                bool result = new RepositoryCatalogBll().ExistRoot(CurrentWeixinPlat.WeixinPlatId);
                ViewBag.ExistRoot = result;
                ViewBag.preview = UrlBuilder.CreateMobileUrl("/RepositoryArticle/MobileRepositoryArticleList", CurrentWeixinPlat.WeixinPlatId, "Preview");
                ViewBag.CopyUrl = UrlBuilder.CreateMobileUrl("/RepositoryArticle/MobileRepositoryArticleList", CurrentWeixinPlat.WeixinPlatId, Guid.NewGuid().ToString());
                return View();
            } 
            #endregion

        #region 添加/更新子目录
        /// <summary>
        /// 添加/更新子目录
         /// </summary>
         /// <returns></returns>
        public QxunJsonResult InsertOrUpdateRepositoryCatalogEntity(string json) {
            var result = new RepositoryCatalogBll().InsertOrUpdateEntity(json);
            return new QxunJsonResult(result);
        }
        #endregion

        #region 获取目录列表
        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <returns></returns>
        public QxunJsonResult GetRepositoryCatatoryEntities()
        {
            var result = new RepositoryCatalogBll().GetEntitiesByFId(CurrentWeixinPlat.WeixinPlatId);
            return new QxunJsonResult(result);
        }
        #endregion

        #region 删除目录
         /// <summary>
        /// 删除目录
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public QxunJsonResult DeleteRepositoryCatalogEntityById(string id)
        {
            var result = new RepositoryCatalogBll().DeleteRepositoryCatalogEntityById(id, CurrentWeixinPlat.WeixinPlatId);
            return new QxunJsonResult(result);
        } 
        #endregion

        #region 根据目录Id获取下面的条目
        /// <summary>
        ///根据目录Id获取下面的条目 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="searchJson"></param>
        /// <returns></returns>
        public QxunJsonResult GetRepositoryArticleEntities(int pageSize, int page, string searchJson,string catalogId = "")
        {
            var lambda = GetWhereLambda(searchJson);
            var result = new RepositoryArticleBll().GetRepositoryArticlePageList(pageSize, page, catalogId, CurrentWeixinPlat.WeixinPlatId, lambda);
            return new QxunJsonResult(result);
        }

        /// <summary>
        /// 拼接WhereLambda搜索条件
        /// </summary>
        /// <param name="searchJson">搜索条件</param>
        /// <returns>返回拼接完成的WhereLambda搜索条件</returns>
        private static System.Linq.Expressions.Expression<Func<RepositoryArticle, bool>> GetWhereLambda(string searchJson)
        {
            var whereLambda = PredicateBuilder.True<RepositoryArticle>();
            if (!string.IsNullOrEmpty(searchJson))
            {
                whereLambda = whereLambda.And(e => e.Title.Contains(searchJson) || e.Contents.Contains(searchJson));
            }
            return whereLambda;
        }
        #endregion

        #region 添加/更新条目
        /// <summary>
        /// 添加/更新条目
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public QxunJsonResult InsertOrUpdateRepositoryAricleEntity(string json)
        {
            ViewRepositoryArticle entity = JsonHelper.DeserializeObject<ViewRepositoryArticle>(json);
            entity.UploadTime = DateTime.Now;
            entity.UploadName = CurrentAccount.NickName;
            var result = new RepositoryArticleBll().InsertOrUpdateEntity(entity);
            return new QxunJsonResult(result);
        } 
        #endregion

        #region 根据条目Id删除条目
        public QxunJsonResult DeleteRepositoryArticleEntityById(string id)
        {
            var result = new RepositoryArticleBll().DeleteEntity(id);
            return new QxunJsonResult(result);
        } 
        #endregion

        #region 根据条目Id获取实体
        /// <summary>
        /// 根据条目Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QxunJsonResult GetRepositoryEntityById(string id)
        {
            var result = new RepositoryArticleBll().GetRepositoryArticleEntityById(id);
            return new QxunJsonResult(result);
        } 
        #endregion

        #endregion
    }
}
