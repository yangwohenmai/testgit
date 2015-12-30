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
    public partial class Dealer : BaseModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required(ErrorMessage = "请填写")]
        public int DealerId { get; set; }
        /// <summary>
        /// BP代码
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过32字")]
        public string BPCode { get; set; }
        ///// <summary>
        ///// 服务站代码
        ///// </summary>
        ////[StringLength(64, ErrorMessage = "不得超过32字")]
        //public string ServerSiteCode { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过64字")]
        public string SiteName { get; set; }

        /// <summary>
        /// 站点负责人
        /// </summary>
        public string SiteManager { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 站点地址
        /// </summary>
        public string SiteAddress { get; set; }
        /// <summary>
        /// 站点创建日期
        /// </summary>
        public DateTime EstablishmentTime { get; set; }

        /// <summary>
        /// 机组型号
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 站点类型
        /// </summary>
        public string SiteType { get; set; }
        /// <summary>
        /// 站点等级
        /// </summary>
        public string SiteGrade { get; set; }
        /// <summary>
        /// 区域服务经理
        /// </summary>
        public string LocalManager { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }
    }
}
