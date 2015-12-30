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
    public class PartInformation : BaseModel

    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Required[ErrorMessage="请填写"]             
        public int PartId { get;set;}
        /// <summary>
        /// 零件号
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 零件价格
        /// </summary>
        [Range(1, 10000), DataType(DataType.Currency)]
        //[DataType(DataType.Currency)]
        public decimal Price { get; set; }
        /// <summary>
        /// 零件头像编号
        /// </summary>
        //[Range(1, int.MaxValue, ErrorMessage = "请上传头像")]
        [Association("Resource", "PartAvata", "ResourceId", IsForeignKey = true)]
        public int PartAvata { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        [QueryAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [EditAuthoritySessionKey("WeixinPlat", "WeixinPlatId")]
        [Association("WeixinPlat", "WeixinPlatId", "WeixinPlatId", IsForeignKey = true)]
        public int WeixinPlatId { get; set; }


    }
}
