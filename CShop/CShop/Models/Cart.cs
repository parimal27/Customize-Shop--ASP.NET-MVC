//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int CartID { get; set; }
        public string uid { get; set; }
        public string pname { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> qty { get; set; }
        public byte[] timestamp { get; set; }
        public Nullable<int> spid1 { get; set; }
        public Nullable<int> spid2 { get; set; }
        public string image { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    }
}