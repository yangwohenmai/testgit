using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.Carrier.Public
{
    /// <summary>
    /// 工单分类
    /// </summary>
    public enum WorkOrderType
    {
        /// <summary>
        /// 保内
        /// </summary>
        [Description("保内")]
        Paone,
        /// <summary>
        /// 保外
        /// </summary>
        [Description("保外")]
        Warranty,
        /// <summary>
        /// 安装验收
        /// </summary>
        [Description("安装验收")]
        InstallationAcceptance
    }

    /// <summary>
    /// 工单状态
    /// </summary>
    public enum WorkOrderState
    {
        /// <summary>
        /// 创建
        /// </summary>
        [Description("创建")]
        Create,
        /// <summary>
        /// 派工
        /// </summary>
        [Description("派工")]
        Dispatching,
        /// <summary>
        /// 审核完成
        /// </summary>
        [Description("审核完成")]
        AuditCompleted
    }
}
