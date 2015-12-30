using CarrierCore.Models;
using Qxun.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierCore.ViewModels
{
    [NotMapped]
    public class ViewHotLine : HotLine, IViewModel
    {
        public new string TicketId { get; set; }
        public new string WeixinPlatId { get; set; }
    }
}
