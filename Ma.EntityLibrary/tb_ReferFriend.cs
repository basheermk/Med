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
    
    public partial class tb_ReferFriend
    {
        public long ReferId { get; set; }
        public long FriendId { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public long PointId { get; set; }
        public bool UsedStatus { get; set; }
    
        public virtual tb_Login tb_Login { get; set; }
        public virtual tb_Login tb_Login1 { get; set; }
        public virtual tb_Points tb_Points { get; set; }
    }
}
