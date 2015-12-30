using Qxun.App.Weistone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.Models
{
    public partial class KnowledgeBaseContext:WeixinPlatContext
    {
        public virtual DbSet<RepositoryArticle> RepositoryArticle { get; set; }
        public virtual DbSet<RepositoryCatalog> RepositoryCatalog { get; set; }

    }
}
