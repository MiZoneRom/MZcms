using MZcms.Entity;
using MZcms.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.Service
{
    public class UserService : ServiceBase, IUserService
    {

        public Users GetUser(long userId)
        {
            return (from a in Context.Users where a.Id == userId select a).FirstOrDefault();
        }

    }
}
