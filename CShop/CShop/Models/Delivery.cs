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
    
    public partial class Delivery
    {
        public int Id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string pname { get; set; }
        public string price { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<long> mobile { get; set; }
    }
}
