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
    
    public partial class tb_Subtopic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Subtopic()
        {
            this.tb_PracticeAttempt = new HashSet<tb_PracticeAttempt>();
            this.tb_UnlockedLevel = new HashSet<tb_UnlockedLevel>();
        }
    
        public long SubTopicID { get; set; }
        public long TopicID { get; set; }
        public string SubTopicName { get; set; }
        public bool Isactive { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Pdfpath { get; set; }
        public string Videopath { get; set; }
        public string YouTubeVideo { get; set; }
        public string FileNameOrginalPDF { get; set; }
        public long CourseID { get; set; }
        public long PackageID { get; set; }
        public long SubjectID { get; set; }
    
        public virtual tb_Course tb_Course { get; set; }
        public virtual tb_Package tb_Package { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_PracticeAttempt> tb_PracticeAttempt { get; set; }
        public virtual tb_Subjects tb_Subjects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UnlockedLevel> tb_UnlockedLevel { get; set; }
        public virtual tb_Topic tb_Topic { get; set; }
    }
}