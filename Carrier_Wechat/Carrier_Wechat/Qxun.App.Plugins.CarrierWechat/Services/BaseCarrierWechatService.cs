using Qxun.App.Plugins.CarrierWechat.Models;
using Qxun.Core.Models;
using Qxun.Core.Repositories;
using Qxun.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.Services
{
    public class BaseCarrierWechatService<T, V, R> : BaseService<T, V, CarrierWechatContext, R>
        where T : BaseModel,IModel,new()
        where V : BaseModel, IViewModel, new()
        where R : IRepository<T, CarrierWechatContext>, new()
    {
        protected override string[] SessionNames
        {
            get 
            { 
                return new string[]{"Weixinplat"};
            }
        }

    }
}
