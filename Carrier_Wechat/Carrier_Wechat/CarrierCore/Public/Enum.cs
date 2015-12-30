using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Public
{
    public enum RecordType
    {
        [Description("授权经销商管理")]
        Dealer,
        [Description("维修项工时管理")]
        Repair,
        [Description("零件信息管理")]
        Part,
        [Description("机组信息管理")]
        Unit
    }
}
