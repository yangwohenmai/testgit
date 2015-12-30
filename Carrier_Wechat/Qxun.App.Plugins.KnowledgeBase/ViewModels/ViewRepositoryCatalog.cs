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
    public class ViewRepositoryCatalog : RepositoryCatalog, IViewModel
    {
        /// <summary>
        /// 目录ID，主键,自增
        /// </summary>
        public new string Id { get; set; }

        /// <summary>
        /// 树形目录上级ID
        /// </summary>
        public new string ParentId { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        public new string WeixinPlatId { get; set; }
    }
}
