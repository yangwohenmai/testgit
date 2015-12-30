using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.Core.Models;
using Qxun.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxun.App.Plugins.KnowledgeBase.Repositories
{
    public class BaseKnowledgeBaseRepository<T> : BaseRepository<T, KnowledgeBaseContext>
        where T : BaseModel, new()
    {
    }
}
