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
    
    public partial class Telai_LastStatus_vw
    {
        public int ID { get; set; }
        public string Telaio { get; set; }
        public string Modello { get; set; }
        public string LastStatus { get; set; }
        public Nullable<int> IDCommessa { get; set; }
        public string Chiave { get; set; }
        public string Annotazioni { get; set; }
        public Nullable<int> Foto { get; set; }
        public Nullable<int> Ingresso { get; set; }
        public Nullable<int> Lavorazione { get; set; }
        public Nullable<int> BollaX { get; set; }
        public Nullable<int> BollaDa { get; set; }
        public Nullable<int> Consegna { get; set; }
        public string Targa { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DataPC { get; set; }
        public bool VerificatoCliente { get; set; }
        public bool IsUrgente { get; set; }
        public bool VerificatoNSG { get; set; }
    }
}
