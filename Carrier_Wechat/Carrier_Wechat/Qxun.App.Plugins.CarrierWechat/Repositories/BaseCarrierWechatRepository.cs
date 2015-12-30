using Qxun.App.Plugins.CarrierWechat.Models;
using Qxun.Core.Models;
using Qxun.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierWechat.Repositories
{
    public class BaseCarrierWechatRepository<T>:BaseRepository<T,CarrierWechatContext>
        where T:BaseModel,new()
    {

    }
}
