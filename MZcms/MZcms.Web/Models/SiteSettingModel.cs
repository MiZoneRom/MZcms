using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.Web.Models
{
    public class SiteSettingModel
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// ICP编号
        /// </summary>
        public string ICPNubmer { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public bool SiteIsOpen { get; set; }

        /// <summary>
        /// 注册方式
        /// </summary>
        public MZcms.Model.SiteSettingsInfo.RegisterTypes RegisterType { get; set; }

        /// <summary>
        /// 手机是否需验证
        /// </summary>
        public bool MobileVerifOpen { set; get; }

        /// <summary>
        /// 邮箱是否必填
        /// </summary>
        public bool RegisterEmailRequired { get; set; }

        /// <summary>
        /// 邮箱是否需要验证
        /// </summary>
        public bool EmailVerifOpen { set; get; }

        /// <summary>
        /// 微信AppId
        /// </summary>
        public string WeixinAppId { get; set; }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public string WeixinAppSecret { get; set; }

        public string Logo
        {
            get;
            set;
        }

        public string QRCode
        {
            get;
            set;
        }

        public string FlowScript
        {
            get;
            set;
        }

        public string Site_SEOTitle
        {
            get;
            set;
        }

        public string Site_SEOKeywords
        {
            get;
            set;
        }

        public string Site_SEODescription
        {
            get;
            set;
        }

        /// <summary>
        /// 客服电话
        /// </summary>
        public string SitePhone { get; set; }

    }
}
