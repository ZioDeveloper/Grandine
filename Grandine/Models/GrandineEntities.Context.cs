﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GRANDINEEntities : DbContext
    {
        public GRANDINEEntities()
            : base("name=GRANDINEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ClassiUtente> ClassiUtente { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<Bisarchista> Bisarchista { get; set; }
        public virtual DbSet<Citta> Citta { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<ClientiXTecnici> ClientiXTecnici { get; set; }
        public virtual DbSet<Commesse> Commesse { get; set; }
        public virtual DbSet<CommesseXTecnici> CommesseXTecnici { get; set; }
        public virtual DbSet<CommesseXClienti> CommesseXClienti { get; set; }
        public virtual DbSet<CommesseXBisarchisti> CommesseXBisarchisti { get; set; }
        public virtual DbSet<Carrozzeria> Carrozzeria { get; set; }
        public virtual DbSet<CommesseXCarrozzerie> CommesseXCarrozzerie { get; set; }
        public virtual DbSet<StoricoStatus> StoricoStatus { get; set; }
        public virtual DbSet<FotoXTelaio> FotoXTelaio { get; set; }
        public virtual DbSet<TipiDocumento> TipiDocumento { get; set; }
        public virtual DbSet<FotoXTelaio_vw> FotoXTelaio_vw { get; set; }
        public virtual DbSet<Telai_LastStatus_vw> Telai_LastStatus_vw { get; set; }
        public virtual DbSet<StoricoStatus_vw> StoricoStatus_vw { get; set; }
        public virtual DbSet<Tecnici> Tecnici { get; set; }
        public virtual DbSet<TelaiAnagrafica> TelaiAnagrafica { get; set; }
        public virtual DbSet<Carglass> Carglass { get; set; }
        public virtual DbSet<Ricambi> Ricambi { get; set; }
        public virtual DbSet<Gravita> Gravita { get; set; }
        public virtual DbSet<TelaiAnagraficaFlat_vw> TelaiAnagraficaFlat_vw { get; set; }
    }
}
