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
    
    public partial class tb_Division
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Division()
        {
            this.tb_DivisionStudent = new HashSet<tb_DivisionStudent>();
        }
    
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int OrderValue { get; set; }
        public long ClassId { get; set; }
        public long SchoolId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tb_Class tb_Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DivisionStudent> tb_DivisionStudent { get; set; }
        public virtual tb_School tb_School { get; set; }
    }
}
