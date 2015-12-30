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
    public partial class RepairTime : BaseModel
    {
        /// <summary>
        /// 维修项工时编号(自增主键)
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
       public int RepairTimeId { get; set; }
       /// <summary>
       /// 维修项工时
       /// </summary>
        public string RepairTimes { get; set; }
       /// <summary>
       /// 维修项类型编号
       /// </summary>
        public int RepairTypeId { get; set; }
       /// <summary>
       /// 维修项编号
       /// </summary>
        public int RepairId { get; set; }
        /// <summary>
        /// 平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }
    }
}
