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
    
    public partial class tb_Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Answer()
        {
            this.tb_UserAttend = new HashSet<tb_UserAttend>();
            this.tb_UserExamAttend = new HashSet<tb_UserExamAttend>();
            this.tb_UserPreparationAttend = new HashSet<tb_UserPreparationAttend>();
        }
    
        public long AnswerId { get; set; }
        public string Answer { get; set; }
        public long QuestionId { get; set; }
        public long AnswerTypeId { get; set; }
        public int RightStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public string AnswerImage { get; set; }
    
        public virtual tb_Question tb_Question { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserAttend> tb_UserAttend { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserExamAttend> tb_UserExamAttend { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserPreparationAttend> tb_UserPreparationAttend { get; set; }
    }
}