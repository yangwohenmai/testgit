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
    public partial class UnitInformation : BaseModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
        public int UnitId { get; set; }
        /// <summary>
        /// 机组编号
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过32字")]
        public string UnitName { get; set; }
        /// <summary>
        /// 机组名称
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过32字")]
        public string UnitNumber { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过64字")]
        public string Specifications { get; set; }

        /// <summary>
        /// 机组生产商
        /// </summary>
        public string UnitProducer { get; set; }
        /// <summary>
        /// 机组类型
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProduceTime { get; set; }
        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime InstallTime { get; set; }

        /// <summary>
        /// 机组型号
        /// </summary>
        public string UnitModel { get; set; }
        /// <summary>
        /// 经销商代码
        /// </summary>
        public string DealerNumber { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string QualityGuaranteePeriod { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime DeadLine { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }
    }
}
