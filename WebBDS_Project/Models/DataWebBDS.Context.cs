﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBDS_Project.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class davisoft_bdsprojectEntities2 : DbContext
    {
        public davisoft_bdsprojectEntities2()
            : base("name=davisoft_bdsprojectEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Audit> Audits { get; set; }
        public DbSet<BDSAccount> BDSAccounts { get; set; }
        public DbSet<BDSApply> BDSApplies { get; set; }
        public DbSet<BDSArea> BDSAreas { get; set; }
        public DbSet<BDSBank> BDSBanks { get; set; }
        public DbSet<BDSBranch> BDSBranches { get; set; }
        public DbSet<BDSCareer> BDSCareers { get; set; }
        public DbSet<BDSComment> BDSComments { get; set; }
        public DbSet<BDSEducation> BDSEducations { get; set; }
        public DbSet<BDSEmper> BDSEmpers { get; set; }
        public DbSet<BDSEmployerInformation> BDSEmployerInformations { get; set; }
        public DbSet<BDSEvent> BDSEvents { get; set; }
        public DbSet<BDSExtNew> BDSExtNews { get; set; }
        public DbSet<BDSLanguage> BDSLanguages { get; set; }
        public DbSet<BDSMarriage> BDSMarriages { get; set; }
        public DbSet<BDSNew> BDSNews { get; set; }
        public DbSet<BDSNewsTypePrice> BDSNewsTypePrices { get; set; }
        public DbSet<BDSNewsType> BDSNewsTypes { get; set; }
        public DbSet<BDSPersonalInformation> BDSPersonalInformations { get; set; }
        public DbSet<BDSPicture> BDSPictures { get; set; }
        public DbSet<BDSSalary> BDSSalaries { get; set; }
        public DbSet<BDSScope> BDSScopes { get; set; }
        public DbSet<BDSTimeWork> BDSTimeWorks { get; set; }
        public DbSet<BDSTransactionHistory> BDSTransactionHistories { get; set; }
        public DbSet<BDSTransaction> BDSTransactions { get; set; }
        public DbSet<BDSTypeContact> BDSTypeContacts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryText> CountryTexts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<DistrictText> DistrictTexts { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationText> LocationTexts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<StateText> StateTexts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
