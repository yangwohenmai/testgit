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
    public partial class RepairType : BaseModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
        public int RepairTypeId { get; set; }
        /// <summary>
        /// 维修项机组型号
        /// </summary>
        public string RepairUnitType { get; set; }
        /// <summary>
        /// 维修项机组编号
        /// </summary>
        public string RepairUnitCode { get; set; }
        /// <summary>
        /// 维修项机组号
        /// </summary>
        public string RepairUnitNum { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string materialNum { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }

    }
}
