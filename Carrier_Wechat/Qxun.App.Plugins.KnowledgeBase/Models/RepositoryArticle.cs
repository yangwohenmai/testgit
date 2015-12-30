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
    public partial class RepositoryArticle : BaseModel
    {
        /// <summary>
        /// 主键,自增
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, ErrorMessage = "标题不得超过50字")]
        public string Title { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 阅读量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        [StringLength(20, ErrorMessage = "上传人名字不得超过20字")]
        public string UploadName { get; set; }

        /// <summary>
        /// 所在目录Id
        /// </summary>
        public int CatalogId { get;set;}

        /// <summary>
        /// 封面图片
        /// </summary>
        public int CoverImg { get; set; }

        /// <summary>
        /// 是否显示(0:不显示1：显示)
        /// </summary>
        public int Display { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [StringLength(1024, ErrorMessage = "摘要不得超过1024字")]
        public string Abstract { get; set; }

        /// <summary>
        /// 微信公众号平台ID
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }



    }
}
