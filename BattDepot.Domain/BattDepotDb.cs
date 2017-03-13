using System.Configuration;
using System.Data.Entity;
using System.Web;
using NS.Entity;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain
{
    public class Davisoft_BDSProjectDb : DbContext
    {
        public Davisoft_BDSProjectDb()
        {
            
            // hook up the Migrations configuration
            Database.SetInitializer<Davisoft_BDSProjectDb>(null);
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }

        //public Davisoft_BDSProjectDb(string connectionName)
        //    : base("name=" + connectionName)
        //{
        //}
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Menu> MenuItems { get; set; }
        public DbSet<Language> Languages { get; set; }
        //public DbSet<CarModel> CarModels { get; set; }
        //public DbSet<CarColor> CarColors { get; set; }
        //public DbSet<Country> Countries { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        protected override void OnModelCreating(DbModelBuilder mb)
        {
            //EnumerationTypeConfiguration.Apply(this, mb);

            mb.Entity<User>().HasMany(u => u.Roles).WithMany().Map(map => map.ToTable("Users_Roles").MapLeftKey("User_Id").MapRightKey("Role_Id"));

            mb.Entity<User>().HasMany(u => u.Branches).WithMany().Map(map => map.ToTable("Users_Branches").MapLeftKey("User_Id").MapRightKey("Branch_Id"));

            mb.Entity<Role>().HasMany(r => r.Permissions).WithMany().Map(map => map.ToTable("Roles_Permissions").MapLeftKey("Role_Id").MapRightKey("Permission_Id"));

            mb.Entity<Menu>().HasMany(m => m.Roles).WithMany().Map(map => map.ToTable("Menus_Roles").MapLeftKey("Menu_Id").MapRightKey("Role_Id"));

            //mb.Entity<CarModel>().HasMany(m => m.Colors).WithMany().Map(map => map.ToTable("Models_Colors").MapLeftKey("Model_Id").MapRightKey("Color_Id"));
            
            //mb.ComplexType<InvoiceBookingModel>();

            //mb.Entity<Booking>().HasMany(b => b.BookingStandardItems).WithMany().Map(map => map.ToTable("Booking_StandardItems").MapLeftKey("Booking_Id").MapRightKey("StandardItem_Id"));
            //mb.Entity<Booking>().HasMany(b => b.BookingOptionalItems).WithMany().Map(map => map.ToTable("Booking_OptionalItems").MapLeftKey("Booking_Id").MapRightKey("OptionalItem_Id"));

            //mb.Entity<ModelQuotation>().HasMany(c => c.QuotationAttribute).WithMany(p => p.ModelQuotations).Map(map => map.ToTable("ModelQuotationAttributes").MapLeftKey("ModelQuotationID").MapRightKey("QuotationAttributeID"));
            //mb.Entity<QuotationAttribute>().HasMany(c => c.ModelQuotations).WithMany().Map(map => map.ToTable("modelquotationattributes").MapRightKey("QuotationAttributeID"));
            //  modelBuilder.Entity<Course>().
            //HasMany(c => c.Students).
            //WithMany(p => p.CoursesAttending).
            //Map(
            // m =>
            // {
            //     m.MapLeftKey("CourseId");
            //     m.MapRightKey("PersonId");
            //     m.ToTable("PersonCourses");
            // });
            //mb.Entity<Country>().HasMany(m => m.Languages).WithMany().Map(map => map.ToTable("Countries_Languages").MapLeftKey("Country_Id").MapRightKey("Language_Id"));
        }
    }
}
