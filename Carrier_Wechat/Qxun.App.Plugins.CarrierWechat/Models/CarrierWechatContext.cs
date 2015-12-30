using Qxun.App.Weistone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.Models
{
    public class CarrierWechatContext: WeixinPlatContext
    {
        public virtual DbSet<MessageBoard> MessageBoard { get; set; }
    }
}