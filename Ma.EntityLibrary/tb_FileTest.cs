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
    
    public partial class tb_FileTest
    {
        public long FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Nullable<long> FileType { get; set; }
        public Nullable<System.Guid> ParentGuid { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
    }
}
