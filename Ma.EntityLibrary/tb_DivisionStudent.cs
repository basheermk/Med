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
    
    public partial class tb_DivisionStudent
    {
        public long DivStudentId { get; set; }
        public long DivisionId { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tb_Division tb_Division { get; set; }
        public virtual tb_Login tb_Login { get; set; }
    }
}