﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Davisoft_BDSProject.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Davisoft_BDSProjectEntities : DbContext
    {
        public Davisoft_BDSProjectEntities()
            : base("name=Davisoft_BDSProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__migrationhistory> C__migrationhistory { get; set; }
        public DbSet<audit> audits { get; set; }
        public DbSet<branch> branches { get; set; }
        public DbSet<counter> counters { get; set; }
        public DbSet<country> countries { get; set; }
        public DbSet<countrytext> countrytexts { get; set; }
        public DbSet<currency> currencies { get; set; }
        public DbSet<district> districts { get; set; }
        public DbSet<districttext> districttexts { get; set; }
        public DbSet<holiday> holidays { get; set; }
        public DbSet<language> languages { get; set; }
        public DbSet<location> locations { get; set; }
        public DbSet<locationtext> locationtexts { get; set; }
        public DbSet<menu> menus { get; set; }
        public DbSet<permission> permissions { get; set; }
        public DbSet<phuong> phuongs { get; set; }
        public DbSet<quarter> quarters { get; set; }
        public DbSet<quartertext> quartertexts { get; set; }
        public DbSet<role> roles { get; set; }
        public DbSet<setting> settings { get; set; }
        public DbSet<state> states { get; set; }
        public DbSet<statetext> statetexts { get; set; }
        public DbSet<user> users { get; set; }
    }
}