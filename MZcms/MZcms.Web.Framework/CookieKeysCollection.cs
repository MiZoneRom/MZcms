
namespace MZcms.Web.Framework
{
    /// <summary>
    /// Cookie集合
    /// </summary>
    public class CookieKeysCollection
    {
        /// <summary>
        /// 平台管理员登录标识
        /// </summary>
        public const string PLATFORM_MANAGER = "MZcms-PlatformManager";

        /// <summary>
        /// 商家管理员登录标识
        /// </summary>
        public const string SELLER_MANAGER = "MZcms-SellerManager";

        /// <summary>
        /// 会员登录标识
        /// </summary>
        public const string MZCMS_USER = "MZcms-User";
        /// <summary>
        /// 会员登录标识
        /// </summary>
        public const string MZCMS_ACTIVELOGOUT = "d783ea20966909ff";  //MZCMS_ACTIVELOGOUT做MD5后的16位字符
        /// <summary>
        /// 分销合作者编号
        /// </summary>
        public const string MZCMS_DISTRIBUTIONUSERLINKIDS = "d2cccb104922d434";   //MZCMS_DISTRIBUTIONUSERLINKIDS做MD5后的16位字符
        /// <summary>
        /// 不同平台用户key
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static string MZCMS_USER_KEY(string platform)
        {
            return string.Format("MZcms-{0}User", platform);
        }
        /// <summary>
        /// 
        /// </summary>
        public const string MZCMS_USER_OpenID = "MZcms-User_OpenId";
        /// <summary>
        /// 购物车
        /// </summary>
        public const string MZCMS_CART = "MZcms-CART";
        /// <summary>
        /// 门店购物车
        /// </summary>
        public const string MZCMS_CART_BRANCH = "MZcms-CART-BRANCH";
        /// <summary>
        /// 商品浏览记录
        /// </summary>
        public const string MZCMS_PRODUCT_BROWSING_HISTORY = "MZcms-ProductBrowsingHistory";
        /// <summary>
        /// 最后产生访问时间
        /// </summary>
        public const string MZCMS_LASTOPERATETIME = "MZcms-LastOpTime";
		/// <summary>
		/// 用户角色(Admin)
		/// </summary>
		public const string USERROLE_ADMIN = "0";
		/// <summary>
		/// 用户角色(SellerAdmin)
		/// </summary>
		public const string USERROLE_SELLERADMIN = "1";
		/// <summary>
		/// 用户角色(User)
		/// </summary>
		public const string USERROLE_USER = "2";
    }
}