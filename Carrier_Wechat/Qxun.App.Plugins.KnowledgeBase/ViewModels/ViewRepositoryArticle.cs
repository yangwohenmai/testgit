using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.ViewModels
{
    [NotMapped]//不映射
    public class ViewRepositoryArticle : RepositoryArticle, IViewModel
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        public new string Id { get; set; }

        /// <summary>
        /// 所在目录Id
        /// </summary>
        public new string CatalogId { get; set; }

        /// <summary>
        /// 封面图片Id
        /// </summary>
        public new string CoverImg { get; set; }

        /// <summary>
        /// 封面图片地址
        /// </summary>
        public string CoverImgUrl { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        public new string WeixinPlatId { get; set; }

    }
}
