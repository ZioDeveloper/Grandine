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
        public virtual DbSet<Tecnici> Tecnici { get; set; }
        public virtual DbSet<ClientiXTecnici> ClientiXTecnici { get; set; }
        public virtual DbSet<Commesse> Commesse { get; set; }
        public virtual DbSet<CommesseXTecnici> CommesseXTecnici { get; set; }
        public virtual DbSet<CommesseXClienti> CommesseXClienti { get; set; }
        public virtual DbSet<CommesseXBisarchisti> CommesseXBisarchisti { get; set; }
        public virtual DbSet<Carrozzeria> Carrozzeria { get; set; }
    }
}
