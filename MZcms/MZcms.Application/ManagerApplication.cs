using MZcms.Core;
using MZcms.Entity;
using MZcms.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.Application
{
    public class ManagerApplication
    {

        private static IManagerService _iManagerService = ObjectContainer.Current.Resolve<IManagerService>();

        /// <summary>
        /// 获取当前登录的平台管理员
        /// </summary>
        /// <returns></returns>
        public static Managers GetPlatformManager(long userId)
        {
            return _iManagerService.GetPlatformManager(userId);
        }

    }
}
