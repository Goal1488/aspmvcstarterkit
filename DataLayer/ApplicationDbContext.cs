using System.Data.Entity;
using Entities.Users;

namespace DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : this(true)
        {
        }

        private ApplicationDbContext(bool enableValidation = true, bool proxyCreationEabled = false,
            bool lazyLoadingEnabled = false, bool validateMigration = true) : base("ftnDb")
        {
            Configuration.ProxyCreationEnabled = proxyCreationEabled;
            Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            Configuration.ValidateOnSaveEnabled = enableValidation;


            if (!validateMigration)
                Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<AppUser> Companies { get; set; }
        
        public static ApplicationDbContext Create(bool enableValidation = true, bool proxyCreationEabled = false, bool lazyLoadingEnabled = false, bool validateMigration = true)
        {
            return new ApplicationDbContext(enableValidation, proxyCreationEabled, lazyLoadingEnabled, validateMigration);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext(true, false, false, true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Company>()
            //    .HasMany(company => company.Users)
            //    .WithOptional(user => user.Company)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Company>()
            //    .HasMany(company => company.Permissions)
            //    .WithRequired(p => p.Company)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<SnapUser>()
            //    .HasMany(user => user.Permissions)
            //    .WithRequired(p => p.SnapUser)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<UserRole>()
            //    .HasMany(m => m.Users)
            //    .WithOptional(m => m.Role)
            //    .Map(config => config.MapKey("RoleId"));

            //modelBuilder.Entity<SnapUser>()
            //    .HasMany(user => user.Documents)
            //    .WithRequired(doc => doc.CreatedByUser)
            //    .HasForeignKey(doc => doc.CreatedByUserGuid)
            //    .WillCascadeOnDelete(false);
        }
    }
}
