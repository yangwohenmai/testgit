using Qxun.App.Plugins.KnowledgeBase.Models;
using Qxun.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qxun.Core.Models;
using Qxun.Core.Repositories;


namespace Qxun.App.Plugins.KnowledgeBase.Services
{
    public class BaseKnowledgeBaseService<T, V, R> : BaseService<T, V, KnowledgeBaseContext, R>
        where T : BaseModel, IModel, new()
        where V : BaseModel, IViewModel, new()
        where R : IRepository<T, KnowledgeBaseContext>, new()
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
