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
    
    public partial class StoricoStatus
    {
        public int ID { get; set; }
        public Nullable<int> IDTelaio { get; set; }
        public string IDStato { get; set; }
        public Nullable<int> IDTecnico { get; set; }
        public string IDUtente { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string InsertUser { get; set; }
    
        public virtual Status Status { get; set; }
        public virtual Utenti Utenti { get; set; }
        public virtual TelaiAnagrafica TelaiAnagrafica { get; set; }
    }
}
