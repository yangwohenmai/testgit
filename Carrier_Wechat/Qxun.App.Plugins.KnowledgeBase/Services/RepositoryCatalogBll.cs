using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.App.Plugins.KnowledgeBase.Repositories;
using Qxun.App.Plugins.KnowledgeBase.ViewModels;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.Services
{
    public class RepositoryCatalogBll : BaseKnowledgeBaseService<RepositoryCatalog, ViewRepositoryCatalog, BaseKnowledgeBaseRepository<RepositoryCatalog>>
    {
        public bool ExistChildren(string parentId) {
            var pId = DesDecodeKey(parentId);
            return Exist(e => e.ParentId == pId);
        }

        /// <summary>
        /// 判断是否存在根节点
        /// </summary>
        /// <param name="platId"></param>
        /// <returns></returns>
        public bool ExistRoot(string platId) {
            var platid = DesDecodeKey(platId);
            return Exist(e => e.WeixinPlatId == platid);
        }
        /// <summary>
        /// 记录子目录条数
        /// </summary>
        public List<int> allCatalogsIds = new List<int>();

        /// <summary>
        /// 获取子目录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platId"></param>
        /// <returns></returns>
        public void GetIdList(string id)
        {
            //解密
            var cId = DesDecodeKey(id);
            allCatalogsIds.Add(cId);
            var entities = GetMany(e => e.ParentId == cId);
            if (entities != null && entities.Count > 0)
            {
                //allCatalogsIds.AddRange(entities.Select(p=>DesDecodeKey(p.Id)).ToList());
                for (int j = 0; j < entities.Count; j++)
                {
                    GetIdList(entities[j].Id);
                }
            }
        }

        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuccessResponseResult DeleteRepositoryCatalogEntityById(string id,string platId) {
            SuccessResponseResult result = new SuccessResponseResult { IsSuccess = true };
            //选查询此Id下面是否有字目录存在
            var isChildrenCatalog = new RepositoryCatalogBll().ExistChildren(id);
            if (isChildrenCatalog)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "此目录下存在子目录，不能删除！";
            }
            else
            {
                bool flag = TransactionHelper<bool>(() =>
                {
                    var u = new RepositoryArticleBll().BatchDealUpdateRepositoryArticle(id, platId);
                    if (u >= 0)
                    {
                        result = new RepositoryCatalogBll().DeleteEntity(id);
                    }
                    return result.IsSuccess;
                });
            }
            return result;
        }

        /// <summary>
        /// 判断该节点是不是根节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsRootCatalog(string id)
        {
            var rId = DesDecodeKey(id);
            bool result = Exist(e => e.Id == rId && e.ParentId == 0);
            return result;
        }
    }
}
