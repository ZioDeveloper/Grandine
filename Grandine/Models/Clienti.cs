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
    
    public partial class Clienti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clienti()
        {
            this.ClientiXTecnici = new HashSet<ClientiXTecnici>();
            this.CommesseXClienti = new HashSet<CommesseXClienti>();
        }
    
        public int ID { get; set; }
        public string Codice { get; set; }
        public string RagioneSociale { get; set; }
        public string CAP { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string Via { get; set; }
        public string Tel1 { get; set; }
        public string Mail1 { get; set; }
        public string Tel2 { get; set; }
        public string Mail2 { get; set; }
        public string PIVA { get; set; }
        public string Fax { get; set; }
        public Nullable<int> GiorniPagamento { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientiXTecnici> ClientiXTecnici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommesseXClienti> CommesseXClienti { get; set; }
    }
}