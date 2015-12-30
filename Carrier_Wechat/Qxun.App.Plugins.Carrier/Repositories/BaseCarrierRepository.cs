using Qxun.App.Plugins.Carrier.Models;
using Qxun.Core.Models;
using Qxun.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.Carrier.Repositories
{
    public class BaseCarrierRepository<T> : BaseRepository<T, CarrierContext>
    where T : BaseModel, new()
    {

    }
}