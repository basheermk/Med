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
    
    public partial class tb_Mark
    {
        public long MarkId { get; set; }
        public Nullable<decimal> Marks { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> QuestionId { get; set; }
        public Nullable<long> AnswerId { get; set; }
    
        public virtual tb_Login tb_Login { get; set; }
    }
}