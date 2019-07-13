﻿using MZcms.Web.Framework;
using System.Web.Mvc;

namespace MZcms.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistrationOrder 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterAreaOrder(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}