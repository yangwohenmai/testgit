using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Entities
{
    public class RepairTypeSearchEntity
    {
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
        public int WeixinPlatId { get; set; }
    }
}
