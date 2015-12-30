using CarrierCore.Models;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.ViewModels
{
    [NotMapped]
    public class ViewRepairTime : RepairTime, IViewModel
    {
        public new string RepairTimeId { get; set; }
        /// <summary>
        /// 维修类型编号（外键）
        /// </summary>
        public new string RepairTypeId { get; set; }
        /// <summary>
        /// 维修项编号（外键）
        /// </summary>
        public new string RepairId { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        public new string WeixinPlatId { get; set; }
    }
}
