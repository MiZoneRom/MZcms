using MZcms.CommonModel;
using MZcms.DTO.QueryModel;
using MZcms.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.IServices
{
    public interface IManagerService : IService
    {
        /// <summary>
        /// 根据查询条件分页获取平台管理员信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        QueryPageModel<Managers> GetPlatformManagers(ManagerQuery query);

        /// <summary>
        /// 获取当前登录的平台管理员
        /// </summary>
        /// <returns></returns>
        Managers GetPlatformManager(long userId);

        /// <summary>
        /// 根据角色ID获取平台角色下的管理员
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IQueryable<Managers> GetPlatformManagerByRoleId(long roleId);

        /// <summary>
        /// 添加一个平台管理员
        /// </summary>
        /// <param name="model"></param>
        void AddPlatformManager(Managers model);

        /// <summary>
        /// 修改平台管理员密码和角色
        /// </summary>
        /// <param name="model"></param>
        void ChangePlatformManagerPassword(long id, string password, long roleId);

        /// <summary>
        /// 删除平台管理员
        /// </summary>
        /// <param name="id"></param>
        void DeletePlatformManager(long id);

        /// <summary>
        /// 批量删除平台管理员
        /// </summary>
        /// <param name="ids"></param>
        void BatchDeletePlatformManager(long[] ids);

        /// <summary>
        /// 是否存在相同的用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckUserNameExist(string userName, bool isPlatFormManager = false);


        /// <summary>
        /// 获取查询的管理员列表
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <returns></returns>
        IQueryable<Managers> GetManagers(string keyWords);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码（明文）</param>
        /// <param name="isPlatFormManager">是否为平台管理员</param>
        /// <returns></returns>
        Managers Login(string username, string password);

    }
}
