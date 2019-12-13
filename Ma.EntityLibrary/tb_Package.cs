//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ma.EntityLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Package()
        {
            this.tb_Level = new HashSet<tb_Level>();
            this.tb_Subjects = new HashSet<tb_Subjects>();
            this.tb_PAYTM_REQUEST = new HashSet<tb_PAYTM_REQUEST>();
            this.tb_PAYTM_RESPONSE = new HashSet<tb_PAYTM_RESPONSE>();
            this.tb_Subtopic = new HashSet<tb_Subtopic>();
            this.tb_Topic = new HashSet<tb_Topic>();
        }
    
        public long PackageID { get; set; }
        public long CourseID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public int ExpiryDays { get; set; }
        public string Description { get; set; }
        public bool Isactive { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public string Files { get; set; }
        public System.DateTime ExpiryDate { get; set; }
    
        public virtual tb_Course tb_Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Level> tb_Level { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Subjects> tb_Subjects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_PAYTM_REQUEST> tb_PAYTM_REQUEST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_PAYTM_RESPONSE> tb_PAYTM_RESPONSE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Subtopic> tb_Subtopic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Topic> tb_Topic { get; set; }
    }
}
