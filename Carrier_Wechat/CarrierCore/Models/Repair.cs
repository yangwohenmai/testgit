using Qxun.Core.Attributes;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Models
{
    public partial class Repair:BaseModel
    {
        /// <summary>
        /// 维修项编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
        public int RepairId { get; set; }
        /// <summary>
        /// 维修项查询号
        /// </summary>
        public string RepairQueryNum { get; set; }
        /// <summary>
        /// 维修项名称
        /// </summary>
        public string RepairName { get; set; }
        /// <summary>
        /// 工作描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }
    }
}
