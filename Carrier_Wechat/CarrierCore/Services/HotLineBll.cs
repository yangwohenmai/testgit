using CarrierCore.Models;
using CarrierCore.ViewModels;
using Qxun.App.Plugins.CarrierCore.Repositories;
using Qxun.App.Plugins.CarrierCore.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Services
{
    public class HotLineBll : BaseCarrierCoreService<HotLine, ViewHotLine, BaseCarrierCoreRepository<HotLine>>
    {
        #region 导出
        public void ExportHotLineInfoInit(out string[] headers, out string[] keyFields, out IQueryable rows, Expression<Func<HotLine, bool>> whereLambda)
        {
            rows = null;
            headers = null;
            keyFields = null;
            IList<ViewHotLine> list = GetHotLine(whereLambda);

            DataTable table = new DataTable();
            List<string> headerTitles = new List<string> { "呼叫单编号", "来电号码", "呼叫单来源", "客户姓名", "来电日期", "来电时间", "来电目的", "支持工程师", "问题描述", "地区", "车牌号", "机组序列号", "信息反馈日期", "信息反馈时间", "车子归属地", "来电客户公司", "咨询机组类型", "维修服务站", "报修事宜", "受理服务站", "受理情况", "是否在四小时内处理",  "回访时间","是否解决故障", "是否解答疑问", "是否提供信息（销售回访）", "是否解答问题（销售回访）", "意见", "评分", "处理结果", "呼叫单状态" };
            List<string> exportFields = new List<string> { "TicketNumber", "CallNumber", "TicketFrom", "CustomerName", "CallDate", "CallTime", "CallFor", "SupportEngineer", "Description", "Location", "PlateNumber", "UnitNumber", "FeedBackDate", "FeedBackTime", "CarOwnership", "CustomerCompany", "UnitType", "ServiceStation", "RepairInfo", "AcceptStation", "AcceptOrNot", "ContactIn4OrNot", "ReturnTime", "SolveOrNot", "AnswerOrNot", "GiveInfoOrNotforSales", "AnswerOrNotforSales", "Suggest", "Score", "Result", "Status" };
            table.Columns.Add("TicketNumber", typeof(string));
            table.Columns.Add("CallNumber", typeof(string));
            table.Columns.Add("TicketFrom", typeof(decimal));
            table.Columns.Add("CustomerName", typeof(string));
            table.Columns.Add("CallDate", typeof(string));
            table.Columns.Add("CallTime", typeof(string));
            table.Columns.Add("CallFor", typeof(string));
            table.Columns.Add("SupportEngineer", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("PlateNumber", typeof(string));
            table.Columns.Add("UnitNumber", typeof(string));
            table.Columns.Add("FeedBackDate", typeof(string));
            table.Columns.Add("FeedBackTime", typeof(string));
            table.Columns.Add("CarOwnership", typeof(string));
            table.Columns.Add("CustomerCompany", typeof(string));
            table.Columns.Add("UnitType", typeof(string));
            table.Columns.Add("ServiceStation", typeof(string));
            table.Columns.Add("RepairInfo", typeof(string));
            table.Columns.Add("AcceptStation", typeof(string));
            table.Columns.Add("AcceptOrNot", typeof(string));
            table.Columns.Add("ContactIn4OrNot", typeof(string));
            table.Columns.Add("ReturnTime", typeof(string));
            table.Columns.Add("SolveOrNot", typeof(string));
            table.Columns.Add("AnswerOrNot", typeof(string));
            table.Columns.Add("GiveInfoOrNotforSales", typeof(string));
            table.Columns.Add("AnswerOrNotforSales", typeof(string));
            table.Columns.Add("Suggest", typeof(string));
            table.Columns.Add("Score", typeof(string));
            table.Columns.Add("Result", typeof(string));
            table.Columns.Add("Status", typeof(string));
            if (list.Count > 0)
            {
                foreach (var hotline in list)
                {
                    DataRow row = table.NewRow();
                    row["TicketNumber"] = hotline.TicketNumber;
                    row["CallNumber"] = hotline.CallNumber;
                    row["TicketFrom"] = hotline.TicketFrom;
                    row["CustomerName"] = hotline.CustomerName;
                    row["CallDate"] = hotline.CallDate;
                    row["CallTime"] = hotline.CallTime;
                    row["CallFor"] = hotline.CallFor;
                    row["SupportEngineer"] = hotline.SupportEngineer;
                    row["Description"] = hotline.Description;
                    row["Location"] = hotline.Location;
                    row["PlateNumber"] = hotline.PlateNumber;
                    row["UnitNumber"] = hotline.UnitNumber;
                    row["FeedBackDate"] = hotline.FeedBackDate;
                    row["FeedBackTime"] = hotline.FeedBackTime;
                    row["CarOwnership"] = hotline.CarOwnership;
                    row["CustomerCompany"] = hotline.CustomerCompany;
                    row["UnitType"] = hotline.UnitType;
                    row["ServiceStation"] = hotline.ServiceStation;
                    row["RepairInfo"] = hotline.RepairInfo;
                    row["AcceptStation"] = hotline.AcceptStation;
                    row["AcceptOrNot"] = hotline.AcceptOrNot;
                    row["ContactIn4OrNot"] = hotline.ContactIn4OrNot;
                    row["ReturnTime"] = hotline.ReturnTime;
                    row["SolveOrNot"] = hotline.SolveOrNot;
                    row["AnswerOrNot"] = hotline.AnswerOrNot;
                    row["GiveInfoOrNotforSales"] = hotline.GiveInfoOrNotforSales;
                    row["AnswerOrNotforSales"] = hotline.AnswerOrNotforSales;
                    row["Suggest"] = hotline.Suggest;
                    row["Score"] = hotline.Score;
                    row["Result"] = hotline.Result;
                    row["Status"] = hotline.Status;
                    table.Rows.Add(row);
                }
            }
            rows = table.AsEnumerable().AsQueryable();
            headers = headerTitles.ToArray();
            keyFields = exportFields.ToArray();
        }
        #endregion


        public IList<ViewHotLine> GetHotLine(Expression<Func<HotLine, bool>> whereLambda)
        {
            return GetMany(whereLambda);
        }
    }
}
