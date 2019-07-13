using MZcms.Entity;
using MZcms.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Web.Areas.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _iTemplateSettingsService;

        public HomeController(IUserService iTemplateSettingsService)
        {
            _iTemplateSettingsService = iTemplateSettingsService;
        }

        // GET: Web/Home
        public ActionResult Index()
        {
            Users userModel = _iTemplateSettingsService.GetUser(1);
            return View();
        }
    }
}