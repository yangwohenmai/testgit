using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.Core.Attributes;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Models
{
    public partial class RecordInfo : BaseModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
        public int RecordId { get; set; }
        /// <summary>
        /// 成功条数
        /// </summary>

        public int SuccessRecord { get; set; }
        /// <summary>
        /// 失败条数
        /// </summary>

        public int FailRecord { get; set; }

        /// <summary>
        /// 总上传条数
        /// </summary>
        //      [Required(ErrorMessage = "请填写文章内容")]
        public int AllUploadRecord { get; set; }
        /// <summary>
        /// 总上传条数
        /// </summary>
        public RecordType Type { get; set; }
        /// <summary>
        /// 总上传条数
        /// </summary>
        public string ErrorInfo { get; set; }


        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }
    }
}
