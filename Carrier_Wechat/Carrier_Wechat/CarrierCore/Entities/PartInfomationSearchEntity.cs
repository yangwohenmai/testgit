using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Entities
{
    public class PartInfomationSearchEntity
    {
        public int PartId { get; set; }
        /// <summary>
        /// 零件编号
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 零件价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 零件图像
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请上传头像")]
        [Association("Resource", "PartAvata", "ResourceId", IsForeignKey = true)]
        public int PartAvata { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        public int WeixinPlatId { get; set; }
    }
}
