using Qxun.App.Weistone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.Carrier.Models
{
    public class CarrierContext : WeixinPlatContext
    {
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }
    }
}

