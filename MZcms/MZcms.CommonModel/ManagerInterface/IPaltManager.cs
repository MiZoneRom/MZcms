using MZcms.Model;
using System.Collections.Generic;

namespace MZcms.CommonModel
{
    public abstract class IPaltManager : BaseModel, IManager
    {
        public List<AdminPrivilege> AdminPrivileges { set; get; }
    }
}
