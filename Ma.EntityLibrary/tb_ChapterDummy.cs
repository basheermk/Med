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
    
    public partial class tb_ChapterDummy
    {
        public long ChapterId { get; set; }
        public string ChapterName { get; set; }
        public long SubjectId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public System.Guid ChapterGuid { get; set; }
        public Nullable<long> OrderValue { get; set; }
    }
}
