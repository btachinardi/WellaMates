using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using WellaMates.Models;

namespace WellaMates.DAL
{
    public class PortalContext : DbContext
    {
        //Global
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<File> Files { get; set; }

        //Refund
        public DbSet<RefundProfile> RefundProfiles { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<RefundAdministrator> RefundAdministrators { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundItem> RefundItems { get; set; }
        public DbSet<RefundItemUpdate> RefundItemUpdates { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Monthly> Monthlies { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<RefundItem>()
                .HasMany(i => i.Files)
                .WithOptional();
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries<UserProfile>();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    entry.Entity.PersonalInfo.CPF = entry.Entity.PersonalInfo.CPF.Replace(".", String.Empty).Replace("-", String.Empty).Trim();
                }
            }
            return base.SaveChanges();
        }
    }
}