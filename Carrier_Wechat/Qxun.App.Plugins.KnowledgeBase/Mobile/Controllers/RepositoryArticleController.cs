using Qxun.App.Plugins.KnowledgeBase.Services;
using Qxun.App.Plugins.KnowledgeBase.ViewModels;
using Qxun.App.Weistone.Mobile.Controllers;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.Util;

namespace Qxun.App.Plugins.KnowledgeBase.Mobile.Controllers
{
    public class RepositoryArticleController : BaseController
    {
        #region 手机端知识库的列表页面
        /// <summary>
        /// 手机端知识库的列表页面
        /// </summary>
        public ActionResult MobileRepositoryArticleList(string u, string r, string a)
        {
            return View();
        }
        #endregion

        #region 手机端知识库的详情页面
        /// <summary>
        /// 手机端知识库的详情页面
        /// </summary>
        /// <returns></returns>

        public ActionResult MobileRepositoryArticleDetail(string u, string r, string a)
        {
            return View();
        }
        #endregion

        #region 根据条件查询条目
        /// <summary>
        /// 根据条件查询条目
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="searchJson"></param>
        /// <param name="catalogId"></param>
        /// <returns></returns>
        public QxunJsonResult GetRepositoryArticleEntities(string u, string r, string a, int pageSize, int page, string searchJson)
        {
            var lambda = GetWhereLambda(searchJson);
            string platId = CurrentWeixinPlat.WeixinPlatId;
            var result = new RepositoryArticleBll().GetEntitiesByFId(page, pageSize, platId, lambda, p => p.Quantity, false);
            return new QxunJsonResult(result, JsonRequestBehavior.AllowGet);
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
            whereLambda = whereLambda.And(e => e.CatalogId > 0);
            return whereLambda;
        }
        #endregion

        #region 根据文章Id获取详情
        /// <summary>
        /// 根据文章Id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QxunJsonResult GetRepositoryArticleEntityById(string u, string r, string a, string id)
        {
            var result = new RepositoryArticleBll().GetRepositoryArticleEntityById(id);
            if (result.IsSuccess && result.ExtInfo!=null)
            {
                new RepositoryArticleBll().UpdateEntity(id, e => { e.Quantity += 1; });
                ViewRepositoryArticle repositoryArticle = result.ExtInfo as ViewRepositoryArticle;
            }
            return new QxunJsonResult(result);
        }
        #endregion
    }
}
