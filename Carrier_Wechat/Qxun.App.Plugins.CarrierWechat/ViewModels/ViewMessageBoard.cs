using Qxun.App.Plugins.CarrierWechat.Models;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.ViewModels
{
    [NotMapped]
    public class ViewMessageBoard : MessageBoard, IViewModel
    {
        public new string Id { get; set; }
        public new string WeixinPlatId { get; set; }
    }
}
