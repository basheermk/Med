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
    
    public partial class tb_CbseAnswer
    {
        public long AnswerId { get; set; }
        public string Answer { get; set; }
        public long QuestionId { get; set; }
        public long AnswerTypeId { get; set; }
        public long AnswerViewId { get; set; }
        public System.Guid AnswerGuid { get; set; }
        public bool RightStatus { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
        public virtual tb_AnswerView tb_AnswerView { get; set; }
        public virtual tb_CbseQuestion tb_CbseQuestion { get; set; }
    }
}
