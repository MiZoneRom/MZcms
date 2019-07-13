
using System.ComponentModel.DataAnnotations.Schema;
namespace MZcms.Model
{
    public partial class SiteSettingsInfo
    {

        /// <summary>
        /// 站点名称
        /// </summary>
        [NotMapped]
        public string SiteName { get; set; }

        /// <summary>
        /// ICP编号
        /// </summary>
        [NotMapped]
        public string ICPNubmer { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        [NotMapped]
        public bool SiteIsClose { get; set; }

        /// <summary>
        /// 注册方式(枚举)
        /// </summary>
        public enum RegisterTypes
        {
            /// <summary>
            /// 普通账号
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 手机账号
            /// </summary>
            Mobile = 1
        }

        /// <summary>
        /// 注册方式
        /// </summary>
        [NotMapped]
        public int RegisterType { get; set; }

        /// <summary>
        /// 手机是否需验证
        /// </summary>
        [NotMapped]
        public bool MobileVerifOpen { set; get; }

        /// <summary>
        /// 邮箱是否必填
        /// </summary>
        [NotMapped]
        public bool RegisterEmailRequired { get; set; }

        /// <summary>
        /// 邮箱是否需要验证
        /// </summary>
        [NotMapped]
        public bool EmailVerifOpen { set; get; }

        /// <summary>
        /// lOGO图片
        /// </summary>
        [NotMapped]
        public string Logo { set; get; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        [NotMapped]
        public string Keyword { set; get; }

        /// <summary>
        /// 热门关键字
        /// </summary>
        [NotMapped]
        public string Hotkeywords { set; get; }

        /// <summary>
        /// 首页页脚
        /// </summary>
        [NotMapped]
        public string PageFoot { get; set; }

        /// <summary>
        /// 微信AppId
        /// </summary>
        [NotMapped]
        public string WeixinAppId { get; set; }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        [NotMapped]
        public string WeixinAppSecret { get; set; }

        /// <summary>
        /// 微信AppId
        /// </summary>
        [NotMapped]
        public string WeixinAppletId { get; set; }

        /// <summary>
        /// 微信AppSecret
        /// </summary>
        [NotMapped]
        public string WeixinAppletSecret { get; set; }

        /// <summary>
        /// 用户Cookie密钥
        /// </summary>
        [NotMapped]
        public string UserCookieKey { get; set; }

        [NotMapped]
        public string QRCode
        {
            get;
            set;
        }

        [NotMapped]
        public string FlowScript
        {
            get;
            set;
        }

        [NotMapped]
        public string Site_SEOTitle
        {
            get;
            set;
        }

        [NotMapped]
        public string Site_SEOKeywords
        {
            get;
            set;
        }

        [NotMapped]
        public string Site_SEODescription
        {
            get;
            set;
        }

        /// <summary>
        /// 客服电话
        /// </summary>
        [NotMapped]
        public string SitePhone { get; set; }

    }
}
