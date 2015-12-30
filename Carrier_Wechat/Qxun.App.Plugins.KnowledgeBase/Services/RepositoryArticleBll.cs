using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.App.Plugins.KnowledgeBase.Repositories;
using Qxun.App.Plugins.KnowledgeBase.ViewModels;
using Qxun.App.Weistone.Services;
using Qxun.App.Weistone.ViewModels;
using Qxun.Core.Common;
using Qxun.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.Services
{
    public class RepositoryArticleBll : BaseKnowledgeBaseService<RepositoryArticle, ViewRepositoryArticle, BaseKnowledgeBaseRepository<RepositoryArticle>>
    {
        /// <summary>
        /// 获取目录和字目录下面的条目
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ListViewResponseResult<ViewRepositoryArticle> GetRepositoryArticlePageList(int pageSize, int page, string catalogId, string weixinPlatId, Expression<Func<RepositoryArticle, bool>> whereLambda)
        {
            //筛选出当前目录的所有子目录
            RepositoryCatalogBll catalog = new RepositoryCatalogBll();
            catalog.GetIdList(catalogId);
            return GetMany(p => catalog.allCatalogsIds.Contains(p.CatalogId), page, pageSize, whereLambda, q => q.Id, false);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuccessResponseResult GetRepositoryArticleEntityById(string id)
        {
            var aId = DesDecodeKey(id);
            var entity = Get(e => e.Id == aId);
            var resource = new ResourceBll().GetResourceEntity(entity.CoverImg);
            if (resource != null)
            {
                entity.CoverImgUrl = resource.ResourceUrl;
            }
            return ToSuccessResponseResult(entity);
        }

        /// <summary>
        /// 批量更新CatalogId
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        public int BatchDealUpdateRepositoryArticle(string cId, string platId)
        {
            var catalogId = DesDecodeKey(cId);
            var weixinPlatId = DesDecodeKey(platId);
            string ctalogId = DesEncodeKey(-1);
            int u;
            if (!new RepositoryCatalogBll().IsRootCatalog(cId))
            {
                u = Update(e => e.CatalogId == catalogId, e => e.CatalogId = ctalogId);
            }
            else
            {
                u = Update(e => e.WeixinPlatId == weixinPlatId, e => e.CatalogId = ctalogId);
            }
            return u;
        }

        /// <summary>
        /// 知识库访问统计新增方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetRepositoryArticleTitle(string id)
        {
            try
            {
                var aId = DesDecodeKey(id);
                var title = Get(e => e.Id == aId).Title;
                return title;
            }
            catch (Exception ex)
            {
                Log4NHelper.Error(ex);
                return "";
            }
        }
    }
}
