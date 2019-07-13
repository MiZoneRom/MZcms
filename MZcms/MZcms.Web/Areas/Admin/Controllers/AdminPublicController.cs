using MZcms.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Web.Areas.Admin.Controllers
{
    public class AdminPublicController : BaseAdminController
    {
        public ActionResult Top()
        {
            ViewBag.UserName = CurrentManager.UserName;
            return View();
        }

        [ChildActionOnly]
        public ActionResult Bottom()
        {
            return View();
        }

    }
}