using Qxun.App.Plugins.Carrier.Public;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.Carrier.Models
{
    /// <summary>
    /// 工单管理
    /// </summary>
    public partial class WorkOrder : BaseModel
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkOrderId { get; set; }

        /// <summary>
        /// 呼叫单号
        /// </summary>
        public string CallNumber { get; set; }

        /// <summary>
        /// 工单来源
        /// </summary>
        public string WorkOrderSource { get; set; }

        /// <summary>
        /// 工单分类
        /// </summary>
        public WorkOrderType WorkOrderType { get; set; }

        /// <summary>
        /// 工单创建日期
        /// </summary>
        public DateTime WorkOrderCreateDate { get; set; }

        /// <summary>
        /// 工单结束日期
        /// </summary>
        public DateTime WorkOrderEndDate { get; set; }

        /// <summary>
        /// 服务网点
        /// </summary>
        public string ServiceOutlets { get; set; }

        /// <summary>
        /// 维修站名称
        /// </summary>
        public string RepairStationName { get; set; }

        /// <summary>
        /// 机组型号
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// 机组序号
        /// </summary>
        public string UnitSerialNumber { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime InstallationDate { get; set; }

        /// <summary>
        /// 维修日期
        /// </summary>
        public DateTime MaintenanceDate { get; set; }
         
        /// <summary>
        /// 信息反馈时间
        /// </summary>
        public DateTime InformationFeedbackDate { get; set; }

        /// <summary>
        /// 客户车子归属地
        /// </summary>
        public string CustomerTheCarBelongsTo { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailedAddress { get; set; }

        /// <summary>
        /// 车辆牌号
        /// </summary>
        public string VehicleBrand { get; set; }

        /// <summary>
        /// 驾驶人姓名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 驾驶人手机
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 来电客户公司
        /// </summary>
        public string CallClientCompany { get; set; }

        /// <summary>
        /// 咨询/报修机组
        /// </summary>
        public string RepairUnit { get; set; }

        /// <summary>
        /// 维修服务站
        /// </summary>
        public string MaintenanceServiceStation { get; set; }

        /// <summary>
        /// 维修人姓名
        /// </summary>
        public string MaintenanceManName { get; set; }

        /// <summary>
        /// 故障现象图片
        /// </summary>
        public string FaultPictures { get; set; }

        /// <summary>
        /// 咨询/报修事宜
        /// </summary>
        public string RepairIssues { get; set; }

        /// <summary>
        /// 是否回复用户
        /// </summary>
        public bool WhetherReplyUser { get; set; }
        
        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime VisitTime { get; set; }

        /// <summary>
        /// 是否解决故障
        /// </summary>
        public bool WhetherSolveFault { get; set; }

        /// <summary>
        /// 是否解答疑问
        /// </summary>
        public bool WhetherAnswerQuestions { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime AuditDate { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditNote { get; set; }

        /// <summary>
        /// 电话回访情况
        /// </summary>
        public string TelephoneVisitSituation { get; set; }

        /// <summary>
        /// 加分情况
        /// </summary>
        public int BonusSituation { get; set; }

        /// <summary>
        /// 是否是直营店
        /// </summary>
        public bool WhetherStores { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 支持工程师
        /// </summary>
        public string SupportEngineer { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public WorkOrderState State { get; set; }
    }
}
