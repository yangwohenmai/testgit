using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.Public
{
    public enum MessageStatus
    {
        /// <summary>
        /// 未回复
        /// </summary>
        [Description("未回复")]
        Unreply,
        /// <summary>
        /// 已回复
        /// </summary>
        [Description("已回复")]
        Replied
    }
}
