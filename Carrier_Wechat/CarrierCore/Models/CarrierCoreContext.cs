using CarrierCore.Models;
using Qxun.App.Weistone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.CarrierCore.Models
{
    public class CarrierCoreContext : WeixinPlatContext
    {
        public virtual DbSet<UnitInformation> UnitInformation { get; set; }

        public virtual DbSet<RecordInfo> RecordInfo { get; set; }

        public virtual DbSet<PartInformation> PartInformation { get; set; }
        public virtual DbSet<Dealer> Dealer { get; set; }
    }
}
