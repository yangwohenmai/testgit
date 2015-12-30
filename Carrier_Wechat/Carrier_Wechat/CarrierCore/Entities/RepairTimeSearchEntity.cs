using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Entities
{
    public class RepairTimeSearchEntity
    {
        public int RepairTimeId { get; set; }
        /// <summary>
        /// 维修项工时
        /// </summary>
        public string RepairTimes { get; set; }
        /// <summary>
        /// 维修项类型编号
        /// </summary>
        [Association("Resource", "RepairTypeId", "ResourceId", IsForeignKey = true)]
        public int RepairTypeId { get; set; }
        /// <summary>
        /// 维修项编号
        /// </summary>
        [Association("Resource", "RepairId", "ResourceId", IsForeignKey = true)]
        public int RepairId { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        public int WeixinPlatId { get; set; }
    }
}
