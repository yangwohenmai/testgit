using Qxun.Core.Attributes;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.Models
{
    public partial class RepositoryCatalog : BaseModel
    {
        /// <summary>
        /// 目录ID，主键,自增
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, ErrorMessage = "标题不得超过50字")]
        public string Title { get; set; }

        /// <summary>
        /// 树形目录上级ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 微信公众号平台ID
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }

    }
}
