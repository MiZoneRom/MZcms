using MZcms.Application;
using MZcms.Core.Helper;
using MZcms.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace MZcms.Web.Framework
{
    public abstract class BaseAdminController : BaseController
    {
        Managers _managers = null;

        public Managers CurrentManager
        {
            get
            {

                if (_managers != null)
                {
                    return _managers;
                }

                var userId = UserCookieEncryptHelper.Decrypt(WebHelper.GetCookie(CookieKeysCollection.PLATFORM_MANAGER), CookieKeysCollection.USERROLE_ADMIN, true);

                if (userId > 0)
                    _managers = ManagerApplication.GetPlatformManager(userId);

                if (null == _managers)
                {
                    WebHelper.DeleteCookie(CookieKeysCollection.PLATFORM_MANAGER);
                    Redirect(Url.Action("Login", new { area = "Admin" }));
                    return null;
                }

                return _managers;

            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            InitVisitorTerminal();

            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            if (CurrentManager == null)
            {
                if (Core.Helper.WebHelper.IsAjax())
                {
                    Result result = new Result();
                    result.msg = "登录超时,请重新登录！";
                    result.success = false;
                    filterContext.Result = Json(result);
                    return;
                }
                else
                {
                    var result = RedirectToAction("", "Login", new { area = "Admin" });
                    filterContext.Result = result;
                    return;
                    //跳转到登录页
                }

            }

            object[] actionFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(UnAuthorize), false);
            if (actionFilter.Length == 1)
                return;
            var controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
            if (CurrentManager.AdminPrivileges == null || CurrentManager.AdminPrivileges.Count == 0 || !AdminPermission.CheckPermissions(CurrentManager.AdminPrivileges, controllerName, actionName))
            {
                if (Core.Helper.WebHelper.IsAjax())
                {
                    Result result = new Result();
                    result.msg = "你没有访问的权限！";
                    result.success = false;
                    filterContext.Result = Json(result);
                    return;
                }
                else
                {
                    //跳转到错误页
                    var result = new ViewResult() { ViewName = "NoAccess" };
                    result.TempData.Add("Message", "你没有权限访问此页面");
                    result.TempData.Add("Title", "你没有权限访问此页面！");
                    filterContext.Result = result;
                    return;
                }
            }


        }

        protected ActionResult SuccessfulRedirectView(string viewName, object routeData = null)
        {
            return RedirectToAction(viewName, routeData);
        }

    }
}
