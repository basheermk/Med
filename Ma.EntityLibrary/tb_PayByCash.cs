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
    
    public partial class tb_PayByCash
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual tb_Login tb_Login { get; set; }
    }
}