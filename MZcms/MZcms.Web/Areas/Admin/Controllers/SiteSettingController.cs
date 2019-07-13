using AutoMapper;
using MZcms.Core.Helper;
using MZcms.IServices;
using MZcms.Model;
using MZcms.Web.Framework;
using MZcms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Web.Areas.Admin.Controllers
{
    public class SiteSettingController : BaseAdminController
    {
        ISiteSettingService _iSiteSettingService;
        public SiteSettingController(ISiteSettingService iSiteSettingService)
        {
            _iSiteSettingService = iSiteSettingService;
        }

        public ActionResult Edit()
        {
            SiteSettingsInfo siteSetting = _iSiteSettingService.GetSiteSettings();
            Mapper.CreateMap<SiteSettingsInfo, SiteSettingModel>().ForMember(a => a.SiteIsOpen, b => b.MapFrom(s => s.SiteIsClose));
            var siteSettingModel = Mapper.Map<SiteSettingsInfo, SiteSettingModel>(siteSetting);
            siteSettingModel.Logo = Core.MZcmsIO.GetImagePath(siteSettingModel.Logo);
            siteSettingModel.QRCode = Core.MZcmsIO.GetImagePath(siteSettingModel.QRCode);
            return View(siteSettingModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(SiteSettingModel siteSettingModel)
        {

            if (string.IsNullOrWhiteSpace(siteSettingModel.Logo))
            {
                return Json(new Result() { success = false, msg = "请上传Logo", status = -2 });
            }

            if (string.IsNullOrEmpty(siteSettingModel.SitePhone))
            {
                return Json(new Result() { success = false, msg = "请填写客服电话", status = -2 });
            }

            string logoName = "logo.png";
            string qrCodeName = "qrCode.png";

            string relativeDir = "/Storage/Plat/Site/";
            string imageDir = relativeDir;

            if (!string.IsNullOrWhiteSpace(siteSettingModel.Logo))
            {

                if (siteSettingModel.Logo.Contains("/Temp/"))
                {
                    string Logo = siteSettingModel.Logo.Substring(siteSettingModel.Logo.LastIndexOf("/Temp"));
                    Core.MZcmsIO.CopyFile(Logo, imageDir + logoName, true);
                }
            }

            if (!string.IsNullOrWhiteSpace(siteSettingModel.QRCode))
            {
                if (siteSettingModel.QRCode.Contains("/Temp/"))
                {
                    string qrCode = siteSettingModel.QRCode.Substring(siteSettingModel.QRCode.LastIndexOf("/Temp"));
                    Core.MZcmsIO.CopyFile(qrCode, imageDir + qrCodeName, true);
                }
            }


            Result result = new Result();
            var siteSetting = _iSiteSettingService.GetSiteSettings();
            siteSetting.SiteName = siteSettingModel.SiteName;
            siteSetting.SitePhone = siteSettingModel.SitePhone;
            siteSetting.SiteIsClose = siteSettingModel.SiteIsOpen;
            siteSetting.Logo = relativeDir + logoName;
            siteSetting.QRCode = relativeDir + qrCodeName;
            siteSetting.FlowScript = siteSettingModel.FlowScript;
            siteSetting.Site_SEOTitle = siteSettingModel.Site_SEOTitle;
            siteSetting.Site_SEOKeywords = siteSettingModel.Site_SEOKeywords;
            siteSetting.Site_SEODescription = siteSettingModel.Site_SEODescription;

            siteSetting.MobileVerifOpen = siteSettingModel.MobileVerifOpen;

            siteSetting.RegisterType = (int)siteSettingModel.RegisterType;
            siteSetting.MobileVerifOpen = false;
            siteSetting.EmailVerifOpen = false;
            siteSetting.RegisterEmailRequired = false;

            switch (siteSettingModel.RegisterType)
            {
                case SiteSettingsInfo.RegisterTypes.Mobile:
                    siteSetting.MobileVerifOpen = true;
                    break;
                case SiteSettingsInfo.RegisterTypes.Normal:
                    if (siteSettingModel.EmailVerifOpen == true)
                    {
                        siteSetting.EmailVerifOpen = true;
                        siteSetting.RegisterEmailRequired = true;
                    }
                    break;
            }

            _iSiteSettingService.SetSiteSettings(siteSetting);
            result.success = true;
            return Json(result);
        }

    }
}