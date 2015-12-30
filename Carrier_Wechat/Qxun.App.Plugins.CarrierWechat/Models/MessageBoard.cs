using Qxun.App.Plugins.CarrierWechat.Public;
using Qxun.Core.Attributes;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.Models
{
    /// <summary>
    /// 留言管理
    /// </summary>
    public partial class MessageBoard:BaseModel
    {
        /// <summary>
        /// 自增主键[留言编号]
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string TelPhone { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [Required(ErrorMessage = "请填写客户姓名")]
        [StringLength(50, ErrorMessage = "客户姓名不得超过50字")]
        public string CustomerName { get; set; }
        
        /// <summary>
        /// 留言内容
        /// </summary>
        [Required(ErrorMessage = "请填写留言内容")]
        public string MessageContent { get; set; }

        /// <summary>
        /// 留言标题
        /// </summary>
        [Required(ErrorMessage = "请填写留言标题")]
        [StringLength(50, ErrorMessage = "留言标题不得超过50字")]
        public string MessageTitle { get; set; }

        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// 留言状态
        /// </summary>
        public MessageStatus MessageStatus { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        /// 用户唯一标识openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 微信平台标识
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; } 
    }
}
