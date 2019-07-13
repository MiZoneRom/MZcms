using MZcms.Core;
using MZcms.IServices;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Application
{
    public static class ServiceApplication
    {

        public static T Create<T>() where T : IService
        {

            T t = MZcms.ServiceProvider.Instance<T>.Create;
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                List<IService> ts = HttpContext.Current.Session["_serviceInstace"] as List<IService>;
                if (ts == null)
                {
                    ts = new List<IService>() { t };
                }
                else
                    ts.Add(t);
                HttpContext.Current.Session["_serviceInstace"] = ts;
            }
            return t;
        }

        public static void DisposeService(ControllerContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            List<IServices.IService> services =System.Web. HttpContext.Current.Session["_serviceInstace"] as List<IServices.IService>;
            if (services != null)
            {
                foreach (var service in services)
                {
                    try
                    {
                        service.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(service.GetType().ToString() + "释放失败！", ex);
                    }
                }
                HttpContext.Current.Session["_serviceInstace"] = null;
            }
        }

    }
}
