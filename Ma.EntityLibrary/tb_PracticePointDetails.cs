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
    
    public partial class tb_PracticePointDetails
    {
        public long PointDetaiId { get; set; }
        public long UserId { get; set; }
        public long LevelId { get; set; }
        public long Points { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public long AttemptId { get; set; }
    
        public virtual tb_Level tb_Level { get; set; }
        public virtual tb_Login tb_Login { get; set; }
        public virtual tb_PracticeAttempt tb_PracticeAttempt { get; set; }
    }
}
