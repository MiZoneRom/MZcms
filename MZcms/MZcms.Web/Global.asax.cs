using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using MZcms.Web.Framework;
using MZcms.Core;
using FluentValidation.Mvc;
using System.Web.Optimization;

namespace MZcms.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("Areas/");
            // 在应用程序启动时运行的代码
            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AreaRegistrationOrder.RegisterAllAreasOrder();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ObjectContainer.ApplicationStart(new AutoFacContainer());

            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new CustomValidatorFactory()));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

        }

        /// <summary>
        /// 自定义验证工厂
        /// </summary>
        public class CustomValidatorFactory : FluentValidation.ValidatorFactoryBase
        {
            public override FluentValidation.IValidator CreateInstance(Type validatorType)
            {
                var type = validatorType.GetGenericArguments()[0];
                var validatorAttribute = type.GetCustomAttribute<FluentValidation.Attributes.ValidatorAttribute>();
                if (validatorAttribute != null)
                {
                    //创建验证实体
                    var obj = System.Activator.CreateInstance(validatorAttribute.ValidatorType);
                    return obj as FluentValidation.IValidator;
                }

                return null;
            }
        }

        protected void Application_End()
        {
            #region 访问首页，重启数据池
            string hosturl = SiteStaticInfo.CurDomainUrl;
#if DEBUG
            Log.Info(System.DateTime.Now.ToString() + " -  " + hosturl);
#endif
            if (!string.IsNullOrWhiteSpace(hosturl))
            {
                System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(hosturl);
                System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            }
            #endregion
        }

#if DEBUG
        protected DateTime dt;
        protected void Application_BeginRequest(Object sender, EventArgs E)
        {
            dt = DateTime.Now;
            //Log.Debug(dt.ToString("yyyy-MM-dd hh:mm:ss fff") + ":[当前请求URL：" + HttpContext.Current.Request.Url + "；请求的参数为：" + HttpContext.Current.Request.QueryString + "；页面开始时间：" + dt.ToString("yyyy-MM-dd hh:mm:ss fff")+"]");

        }
        protected void Application_EndRequest(Object sender, EventArgs E)
        {
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2 - dt;
            if (ts.TotalMilliseconds >= 5000)//5秒以上的慢页面进行记录
                Log.Debug(dt2.ToString("yyyy-MM-dd hh:mm:ss fff") + ":[当前请求URL：" + HttpContext.Current.Request.Url + "；请求的参数为：" + HttpContext.Current.Request.QueryString + "；页面加载的时间：" + ts.TotalMilliseconds.ToString() + " 毫秒]");
        }
#endif

    }
}