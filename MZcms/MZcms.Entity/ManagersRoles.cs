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
    /// ManagersRoles
    /// </summary>
    public partial class ManagersRoles : BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ManagersRoles()
        {
            this.ManagerPrivileges = new HashSet<ManagerPrivileges>();
        }
    
    	/// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }
    	/// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
         /// <summary>
    	 /// 
         /// </summary>
        public virtual ICollection<ManagerPrivileges> ManagerPrivileges { get; set; }
    }
}
