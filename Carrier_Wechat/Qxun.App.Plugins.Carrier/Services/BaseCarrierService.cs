using Qxun.App.Plugins.Carrier.Models;
using Qxun.Core.Models;
using Qxun.Core.Repositories;
using Qxun.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.Carrier.Services
{
    public class BaseCarrierService<T, V, R> : BaseService<T, V, CarrierContext, R>
        where T : BaseModel, IModel, new()
        where V : BaseModel, IViewModel, new()
        where R : IRepository<T, CarrierContext>, new()
    {
        protected override string[] SessionNames
        {
            get
            {
                //本项目包含了两个主要的Session，请按需添加，分别是weixinplat和user，此操作是指定Session给当前项目CRUD验证时使用
                return new string[] { "weixinplat" };
            }
        }
    }
}
