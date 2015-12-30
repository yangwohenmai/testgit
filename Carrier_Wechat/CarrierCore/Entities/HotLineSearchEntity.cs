using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Entities
{
    public class HotLineSearchEntity
    {
        public int TicketId { get; set; }
        /// <summary>
        /// 呼叫单编号
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过32字")]
        public string TicketNumber { get; set; }
        /// <summary>
        /// 来电号码
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过32字")]
        public string CallNumber { get; set; }
        /// <summary>
        /// 呼叫单来源
        /// </summary>
        //[StringLength(64, ErrorMessage = "不得超过64字")]
        public string TicketFrom { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 来电日期
        /// </summary>
        public DateTime CallDate { get; set; }
        /// <summary>
        /// 来电时间
        /// </summary>
        public DateTime CallTime { get; set; }
        /// <summary>
        /// 来电目的
        /// </summary>
        public string CallFor { get; set; }

        /// <summary>
        /// 支持工程师
        /// </summary>
        public string SupportEngineer { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 机组序列号
        /// </summary>
        public DateTime UnitNumber { get; set; }
        /// <summary>
        /// 反馈日期
        /// </summary>
        public DateTime FeedBackDate { get; set; }
        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FeedBackTime { get; set; }
        /// <summary>
        /// 车子归属地
        /// </summary>
        public string CarOwnership { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        public string CustomerCompany { get; set; }
        /// <summary>
        /// 保修机组类型
        /// </summary>
        public string UnitType { get; set; }
        /// <summary>
        /// 维修服务站
        /// </summary>
        public string ServiceStation { get; set; }
        /// <summary>
        /// 报修事宜
        /// </summary>
        public string RepairInfo { get; set; }
        /// <summary>
        /// 受理服务站
        /// </summary>
        public string AcceptStation { get; set; }
        /// <summary>
        /// 受理情况
        /// </summary>
        public string AcceptOrNot { get; set; }
        /// <summary>
        /// 是否4小时内及时联系用户
        /// </summary>
        public string ContactIn4OrNot { get; set; }
        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime ReturnTime { get; set; }
        /// <summary>
        /// 是否解决故障
        /// </summary>
        public string SolveOrNot { get; set; }
        /// <summary>
        /// 是否解答疑问
        /// </summary>
        public string AnswerOrNot { get; set; }
        /// <summary>
        /// 是否提供信息（销售回访）
        /// </summary>
        public string GiveInfoOrNotforSales { get; set; }
        /// <summary>
        /// 是否解答问题（销售回访）
        /// </summary>
        public string AnswerOrNotforSales { get; set; }
        /// <summary>
        /// 意见/建议
        /// </summary>
        public string Suggest { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// 处理结果啊
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 微信平台编号
        /// </summary>
        public int WeixinPlatId { get; set; }
    }
}
