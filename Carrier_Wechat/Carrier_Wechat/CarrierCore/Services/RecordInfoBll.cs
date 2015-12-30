using Qxun.App.Plugins.CarrierCore.Models;
using Qxun.App.Plugins.CarrierCore.Public;
using Qxun.App.Plugins.CarrierCore.Repositories;
using Qxun.App.Plugins.CarrierCore.ViewModels;
using Qxun.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Services
{
    public class RecordInfoBll : BaseCarrierService<RecordInfo, ViewRecordInfo, BaseCarrierRepository<RecordInfo>>
    {
        public RecordInfo GetRecord(string weixinPlatId, RecordType Typeout)
        {
            //Type = RecordType.Dealer;
            int platId = DesDecodeKey(weixinPlatId);
            if (!string.IsNullOrEmpty(weixinPlatId))
                return GetMany(o => o.WeixinPlatId == platId && o.Type == Typeout).OrderByDescending(e => e.UploadTime).FirstOrDefault();
            else
                return null;
        }

        /// <summary>
        /// 插入errer的数据记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuccessResponseResult InsertError(int errorCount, int allCount, string ErrorInfo, string weixinPlatId, RecordType Typeout)
        {
            int platId = DesDecodeKey(weixinPlatId);
            SuccessResponseResult responseResult = new SuccessResponseResult();
            DateTime time = DateTime.Now;
            time =Convert.ToDateTime(time);
            int success = allCount - errorCount;
            var recordEntity = Get(e => e.WeixinPlatId == platId && e.Type == Typeout);
            //if (recordEntity == null)
            responseResult = InsertRecord(allCount, errorCount, success, time, Typeout, ErrorInfo, weixinPlatId);
            //else
                //responseResult = UpdateEntity(recordEntity.RecordId, e => { e.AllUploadRecord = allCount; e.FailRecord = errorCount; e.SuccessRecord = success; e.UploadTime = time; e.Type = RecordType.Dealer; e.ErrorInfo = ErrorInfo; });
            return responseResult;
        }
        public SuccessResponseResult InsertRecord(int allCount, int errorCount, int success, DateTime time, RecordType Typeout, string Error, string weixinPlatId)
        {
            ViewRecordInfo info = new ViewRecordInfo
            {
                AllUploadRecord = allCount,
                FailRecord = errorCount,
                SuccessRecord = success,
                UploadTime = time,
                Type = Typeout,
                ErrorInfo = Error,
                WeixinPlatId = weixinPlatId
            };
            SuccessResponseResult result = InsertEntity(info);
            return result;

        }
    }
}
