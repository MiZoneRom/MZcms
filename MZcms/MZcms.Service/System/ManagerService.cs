using MZcms.CommonModel;
using MZcms.Core;
using MZcms.Core.Helper;
using MZcms.DTO.QueryModel;
using MZcms.Entity;
using MZcms.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.Service
{
    public class ManagerService : ServiceBase, IManagerService
    {
        public QueryPageModel<Managers> GetPlatformManagers(ManagerQuery query)
        {
            int total = 0;
            IQueryable<Managers> users = Context.Managers.FindBy(item => item.Id > 0, query.PageNo, query.PageSize, out total, item => item.RoleId, true);
            QueryPageModel<Managers> pageModel = new QueryPageModel<Managers>()
            {
                Models = users.ToList(),
                Total = total
            };
            return pageModel;
        }

        public IQueryable<Managers> GetPlatformManagerByRoleId(long roleId)
        {
            return Context.Managers.FindBy(item => item.RoleId == roleId);
        }

        public Managers GetPlatformManager(long userId)
        {
            Managers manager = null;
            string CACHE_MANAGER_KEY = CacheKeyCollection.Manager(userId);

            if (Cache.Exists(CACHE_MANAGER_KEY))
            {
                manager = Core.Cache.Get<Managers>(CACHE_MANAGER_KEY);
            }
            else
            {
                manager = Context.Managers.FirstOrDefault(item => item.Id == userId);
                if (manager == null)
                    return null;
                if (manager.RoleId == 0)
                {
                    List<AdminPrivilege> AdminPrivileges = new List<AdminPrivilege>();
                    AdminPrivileges.Add((AdminPrivilege)0);
                    manager.RealName = "系统管理员";
                    manager.AdminPrivileges = AdminPrivileges;
                }
                else
                {
                    var model = Context.Managers.FirstOrDefault(p => p.Id == manager.RoleId);
                    if (model != null)
                    {
                        List<AdminPrivilege> AdminPrivileges = new List<AdminPrivilege>();
                        (from a in Context.ManagerPrivileges where a.RoleId == model.RoleId select a).ToList().ForEach(a => AdminPrivileges.Add((AdminPrivilege)a.Privilege));
                        manager.RealName = model.RealName;
                        manager.AdminPrivileges = AdminPrivileges;
                    }
                }
                Cache.Insert(CACHE_MANAGER_KEY, manager);
            }
            return manager;
        }

        public void AddPlatformManager(Managers model)
        {
            if (model.RoleId == 0)
                throw new MZcmsException("权限组选择不正确!");
            if (CheckUserNameExist(model.UserName, true))
            {
                throw new MZcmsException("该用户名已存在！");
            }
            model.Salt = Guid.NewGuid().ToString();
            model.AddDate = DateTime.Now;
            var pwd = SecureHelper.MD5(model.Password);
            model.Password = SecureHelper.MD5(pwd + model.Salt);
            Context.Managers.Add(model);
            Context.SaveChanges();
        }

        public void ChangePlatformManagerPassword(long id, string password, long roleId)
        {
            var model = Context.Managers.FindBy(item => item.Id == id).FirstOrDefault();
            if (model == null)
                throw new MZcmsException("该管理员不存在，或者已被删除!");
            if (roleId != 0 && model.RoleId != 0)
                model.RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(password))
            {
                var pwd = SecureHelper.MD5(password);
                model.Password = SecureHelper.MD5(pwd + model.Salt);
            }

            Context.SaveChanges();
            string CACHE_MANAGER_KEY = CacheKeyCollection.Manager(id);
            Core.Cache.Remove(CACHE_MANAGER_KEY);
        }

        public void DeletePlatformManager(long id)
        {
            var model = Context.Managers.FindBy(item => item.Id == id && item.RoleId != 0).FirstOrDefault();
            Context.Managers.Remove(model);
            Context.SaveChanges();
            string CACHE_MANAGER_KEY = CacheKeyCollection.Manager(id);
            Core.Cache.Remove(CACHE_MANAGER_KEY);
        }


        public void BatchDeletePlatformManager(long[] ids)
        {
            var model = Context.Managers.FindBy(item => item.RoleId != 0 && ids.Contains(item.Id));
            Context.Managers.RemoveRange(model);
            Context.SaveChanges();
            foreach (var id in ids)
            {
                string CACHE_MANAGER_KEY = CacheKeyCollection.Manager(id);
                Core.Cache.Remove(CACHE_MANAGER_KEY);
            }
        }

        public IQueryable<Managers> GetManagers(string keyWords)
        {
            IQueryable<Managers> managers = Context.Managers.FindBy(item =>
                         (keyWords == null || keyWords == "" || item.UserName.Contains(keyWords)));
            return managers;
        }

        public Managers Login(string username, string password)
        {
            var _iSiteSettingService = ObjectContainer.Current.Resolve<ISiteSettingService>();
            Managers manager = Context.Managers.FindBy(item => item.UserName == username).FirstOrDefault();
            if (manager != null)
            {
                string encryptedWithSaltPassword = GetPasswrodWithTwiceEncode(password, manager.Salt);
                if (encryptedWithSaltPassword.ToLower() != manager.Password)//比较密码是否一致
                    manager = null;//不一致，则置空，表示未找到指定的管理员
                else//一致，则表示登录成功，更新登录时间
                {

                }
            }
            return manager;
        }

        string GetPasswrodWithTwiceEncode(string password, string salt)
        {
            string encryptedPassword = Core.Helper.SecureHelper.MD5(password);//一次MD5加密
            string encryptedWithSaltPassword = Core.Helper.SecureHelper.MD5(encryptedPassword + salt);//一次结果加盐后二次加密
            return encryptedWithSaltPassword;
        }

        public bool CheckUserNameExist(string username, bool isPlatFormManager = false)
        {
            if (isPlatFormManager)
            {
                return Context.Managers.Any(item => item.UserName.ToLower() == username.ToLower());
            }
            return Context.Users.Any(item => item.UserName.ToLower() == username.ToLower());
        }

    }
}
