using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Entities
{
   public class RepairSearchEntity
    {
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
       /// 微信平台号
       /// </summary>
       public int WeixinPlatId { get; set; }
    }
}
