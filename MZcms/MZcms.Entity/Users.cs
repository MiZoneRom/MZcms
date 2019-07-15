//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MZcms.Entity
{
    using System;
    using System.Collections.Generic;
    using MZcms.Model;
    using MZcms.CommonModel;
    /// <summary>
    /// 用户类
    /// </summary>
    public partial class Users : BaseModel
    {
    	/// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }
    	/// <summary>
        /// 用户组ID
        /// </summary>
        public long GroupId { get; set; }
    	/// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    	/// <summary>
        /// 盐值
        /// </summary>
        public string Salt { get; set; }
    	/// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    	/// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
    	/// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    	/// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    	/// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    	/// <summary>
        /// 性别
        /// </summary>
        public bool IsMale { get; set; }
    	/// <summary>
        /// 生日
        /// </summary>
        public Nullable<System.DateTime> Birthday { get; set; }
    	/// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }
    	/// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    	/// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
    	/// <summary>
        /// 账户余额
        /// </summary>
        public Nullable<decimal> Amount { get; set; }
    	/// <summary>
        /// 账户积分
        /// </summary>
        public Nullable<int> Point { get; set; }
    	/// <summary>
        /// 经验
        /// </summary>
        public Nullable<int> Exp { get; set; }
    	/// <summary>
        /// 状态
        /// </summary>
        public Nullable<int> Status { get; set; }
    	/// <summary>
        /// 注册日期
        /// </summary>
        public System.DateTime RegDate { get; set; }
    	/// <summary>
        /// 注册IP
        /// </summary>
        public string RegIP { get; set; }
    	/// <summary>
        /// 上一次登录日期
        /// </summary>
        public Nullable<System.DateTime> LastLoginDate { get; set; }
    
         /// <summary>
    	 /// 
         /// </summary>
        public virtual UserGroups UserGroups { get; set; }
    }
}