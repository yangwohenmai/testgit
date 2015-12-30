using Qxun.App.Plugins.CarrierCore.Models;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.ViewModels
{
    [NotMapped]
    public class ViewPartInformation : PartInformation, IViewModel
    {
        public new string PartId { get; set; }
        /// <summary>
        /// 零件图像编号
        /// </summary>
        public new string PartAvata { get; set; }

        public new string WeixinPlatId { get; set; }
    }
}
