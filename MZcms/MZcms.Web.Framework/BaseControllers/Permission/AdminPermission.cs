using MZcms.CommonModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MZcms.Web.Framework
{
    public class ActionPermission
    {
        public string ActionName { set; get; }
        public string ControllerName { set; get; }
        public string Description { set; get; }
    }

    public static class AdminPermission
    {
        private readonly static Dictionary<AdminPrivilege, IEnumerable<ActionPermission>> privileges;
        private readonly static IEnumerable<ActionPermission> ActionPermissions;
        static AdminPermission()
        {
            ActionPermissions = GetAllActionByAssembly();
            privileges = new Dictionary<AdminPrivilege, IEnumerable<ActionPermission>>();
            var AdminPrivileges = PrivilegeHelper.GetPrivileges<AdminPrivilege>().Privilege.Select(a => a.Items);
            foreach (var privilege in AdminPrivileges)
            {
                foreach (var item in privilege)
                {
                    List<ActionPermission> actions = new List<ActionPermission>();
                    var ctrls = item.Controllers;
                    foreach (var ctrl in ctrls)
                    {
                        foreach (string act in ctrl.ActionNames)
                        {
                            var acts = GetActionByControllerName(ctrl.ControllerName, act);
                            actions.AddRange(acts);
                        }
                    }
                    privileges.Add((AdminPrivilege)item.PrivilegeId, actions);
                }
            }

        }




        private static IEnumerable<ActionPermission> GetActionByControllerName(string controllername, string actionname = "")
        {
            //需要特殊处理的一批模块
            if (controllername.ToLower() == "pagesettings") 
            {
                return ActionPermissions.Where(item => item.ControllerName.ToLower() == controllername.ToLower());
            }
            if (controllername.ToLower() == "gift")
            {
                return ActionPermissions.Where(item => item.ControllerName.ToLower() == controllername.ToLower());
            }
            return ActionPermissions.Where(item => item.ControllerName.ToLower() == controllername.ToLower() && (actionname == "" || item.ActionName.ToLower() == actionname.ToLower()));
        }

        public static Dictionary<AdminPrivilege, IEnumerable<ActionPermission>> Privileges { get { return privileges; } }


        private static IList<ActionPermission> GetAllActionByAssembly()
        {
            var result = new List<ActionPermission>();
            var types = Assembly.Load("MZcms.Web").GetTypes().Where(a => a.BaseType != null && a.BaseType.Name == "BaseAdminController");

            foreach (var type in types)
            {
                var members = type.GetMethods();
                foreach (var member in members)
                {
                    if (member.ReturnType.Name == "ActionResult" || member.ReturnType.Name == "JsonResult")//如果是Action
                    {
                        var ap = new ActionPermission();

                        ap.ActionName = member.Name;
                        ap.ControllerName = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10); // 去掉“Controller”后缀

                        object[] attrs = member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                        if (attrs.Length > 0)
                            ap.Description = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;

                        result.Add(ap);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 检查是否有权限访问该动作的授权
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool CheckPermissions(List<AdminPrivilege> userprivileages, string controllerName, string actionName)
        {
            if (userprivileages.Contains(0))
                return true;
             
            return privileges.Where(a => userprivileages.Contains(a.Key)).Any(b => b.Value.Any(c => c.ControllerName.ToLower() == controllerName.ToLower() && c.ActionName.ToLower() == actionName.ToLower()));
        }

    }
}