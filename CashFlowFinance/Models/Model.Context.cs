﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CashFlowFinance.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CashFlowEntities : DbContext
    {
        public CashFlowEntities()
            : base("name=CashFlowEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AFP> AFP { get; set; }
        public virtual DbSet<Banco> Banco { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Familia> Familia { get; set; }
        public virtual DbSet<Gasto> Gasto { get; set; }
        public virtual DbSet<Impuesto> Impuesto { get; set; }
        public virtual DbSet<Miembro> Miembro { get; set; }
        public virtual DbSet<Ocupacion> Ocupacion { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Tasa> Tasa { get; set; }
        public virtual DbSet<TipoGasto> TipoGasto { get; set; }
    }
}
