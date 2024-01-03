using LocalizeR.Core.Entities;
using LocalizeR.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalizeR.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Rating> RatingValues { get; set; }
        public DbSet<RequestSequence> RequestRecords { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public ApplicationDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Rating>().HasOne(r => r.ServiceProviderProfile)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.ServiceProviderId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelbuilder.Entity<RequestSequence>().HasOne(rs => rs.RequesterProfile).WithMany().HasForeignKey(r => r.RequesterId).OnDelete(DeleteBehavior.NoAction);
            modelbuilder.Entity<RequestSequence>().HasOne(rs => rs.ServiceProfile).WithMany().HasForeignKey(rs => rs.ServiceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
