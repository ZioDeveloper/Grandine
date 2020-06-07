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
    
    public partial class TelaiAnagrafica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TelaiAnagrafica()
        {
            this.FotoXTelaio = new HashSet<FotoXTelaio>();
            this.StoricoStatus = new HashSet<StoricoStatus>();
            this.Ricambi = new HashSet<Ricambi>();
        }
    
        public int ID { get; set; }
        public string Telaio { get; set; }
        public string Modello { get; set; }
        public string InsertUser { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string NomeFile { get; set; }
        public Nullable<int> IDCommessa { get; set; }
        public string Annotazioni { get; set; }
        public Nullable<System.DateTime> DataIn { get; set; }
        public Nullable<System.DateTime> DataOut { get; set; }
        public string NFattAttiva { get; set; }
        public Nullable<System.DateTime> DataFattAtt { get; set; }
        public Nullable<double> ImpFattAtt { get; set; }
        public Nullable<int> IDTecnico { get; set; }
        public Nullable<System.DateTime> DataFatturaPassiva { get; set; }
        public Nullable<double> ImportoFattPass { get; set; }
        public Nullable<int> IDCarrozzeria1 { get; set; }
        public string NumFattCarrozzeria1 { get; set; }
        public Nullable<System.DateTime> DataFatturaCarrozzeria1 { get; set; }
        public Nullable<double> ImportoCarrozzeria1 { get; set; }
        public Nullable<int> IDCarrozzeria2 { get; set; }
        public string NumFattCarrozzeria2 { get; set; }
        public Nullable<System.DateTime> DataFatturaCarrozzeria2 { get; set; }
        public Nullable<double> ImportoCarrozzeria2 { get; set; }
        public Nullable<int> IDCarglass { get; set; }
        public string NumFattCarGlass { get; set; }
        public Nullable<double> ImportoFattCarGlass { get; set; }
        public Nullable<System.DateTime> DataFatturaCarglass { get; set; }
        public Nullable<int> IDBisarchistaAndata { get; set; }
        public string NumFattBisarchistaA { get; set; }
        public Nullable<System.DateTime> DataFattBisarchistaA { get; set; }
        public Nullable<double> CostoAndata { get; set; }
        public Nullable<int> IDBisarchistaRitorno { get; set; }
        public string NumFattBisarchistaR { get; set; }
        public Nullable<System.DateTime> DataFattBisarchistaR { get; set; }
        public Nullable<double> CostoRitorno { get; set; }
        public Nullable<double> Costi { get; set; }
        public string NumFattTecnico { get; set; }
        public string Chiave { get; set; }
        public string Fila { get; set; }
    
        public virtual Bisarchista Bisarchista { get; set; }
        public virtual Bisarchista Bisarchista1 { get; set; }
        public virtual Carrozzeria Carrozzeria { get; set; }
        public virtual Carrozzeria Carrozzeria1 { get; set; }
        public virtual Commesse Commesse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FotoXTelaio> FotoXTelaio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoricoStatus> StoricoStatus { get; set; }
        public virtual Tecnici Tecnici { get; set; }
        public virtual Carglass Carglass { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ricambi> Ricambi { get; set; }
    }
}
