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
    
    public partial class tb_QuestionDummy
    {
        public long QuestionId { get; set; }
        public string Question { get; set; }
        public double Mark { get; set; }
        public long QuestionTypeId { get; set; }
        public System.Guid ParentGuid { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public System.Guid QuestionGuid { get; set; }
        public string Explanation { get; set; }
        public Nullable<double> NegativeMark { get; set; }
        public long QuestionIdOld { get; set; }
    }
}
