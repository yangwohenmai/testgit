using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Entities
{
    public class DealerSearchEntity
    {
        /// <summary>
        /// 经销商Id号
        /// </summary>
        public int DealerId { get; set; }
        /// <summary>
        /// 经销商BP编号
        /// </summary>
        public string BPCode { get; set; }
        ///// <summary>
        ///// 服务站代码
        ///// </summary>
        //public string ServerSiteCode { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 站点负责人
        /// </summary>
        public string SiteManager { get; set; }
        /// <summary>
        /// 联系电话号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 站点地址
        /// </summary>
        public string SiteAddress { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public DateTime? EstablishmentTime { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 站点类型
        /// </summary>
        public string SiteType { get; set; }
        /// <summary>
        /// 站点级别
        /// </summary>
        public string SiteGrade { get; set; }
        /// <summary>
        /// 区域经理
        /// </summary>
        public string LocalManager { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        public int WeixinPlatId { get; set; }
    }
}
