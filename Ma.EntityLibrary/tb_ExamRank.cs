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
    
    public partial class tb_ExamRank
    {
        public long ExamMarkId { get; set; }
        public long ExamId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long Examtime { get; set; }
        public double Total { get; set; }
        public long Rank { get; set; }
    
        public virtual tb_Exam tb_Exam { get; set; }
        public virtual tb_Login tb_Login { get; set; }
    }
}
