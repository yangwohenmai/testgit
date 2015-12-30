using CarrierCore.Models;
using CarrierCore.ViewModels;
using Qxun.App.Plugins.CarrierCore.Repositories;
using Qxun.App.Plugins.CarrierCore.Services;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.Services
{
    public class RepairBll : BaseCarrierService<Repair, ViewRepair, BaseCarrierRepository<Repair>>
    {
        /// <summary>
        /// 获取实体的分页列表数据 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="platId"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public ListViewResponseResult<ViewRepair> GetRepairListPageList(int pageSize, int pageIndex, string platId, Expression<Func<Repair, bool>> whereLambda, Expression<Func<Repair, int>> orderByLambda = null, bool isAsc = false)
        {
            int weixinPlatId = DesDecodeKey(platId);
            var listResult = GetMany(e => e.WeixinPlatId == weixinPlatId, pageIndex, pageSize, whereLambda, orderByLambda, isAsc);
            return listResult;
        }
        /// <summary>
        /// 获取所有维修项信息
        /// </summary>
        /// <param name="weixinPlatId"></param>
        /// <returns></returns>
        public int GetRepairTotalCount(string weixinPlatId)
        {
            int platId = DesDecodeKey(weixinPlatId);
            return GetMany(o => o.WeixinPlatId == platId).Count;
        }
    }
}
