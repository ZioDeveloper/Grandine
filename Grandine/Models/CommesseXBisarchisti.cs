//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Grandine.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CommesseXBisarchisti
    {
        public int ID { get; set; }
        public Nullable<int> IDCommessa { get; set; }
        public Nullable<int> IDBisarchista { get; set; }
    
        public virtual Bisarchista Bisarchista { get; set; }
        public virtual Commesse Commesse { get; set; }
    }
}
