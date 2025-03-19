using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsClub.Models;

namespace StudentsClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvenT> Event { get; set; }
        //public DbSet<Member> Members { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EvenT>()
                .HasOne(o => o.User)
                .WithMany(o => o.Events)
                .HasForeignKey(o => o.UserId);
            //base.OnModelCreating(builder);
            //builder.Entity<Member>()
            //    .HasOne(o => o.User)
            //    .WithOne(o => o.Member);
        }
    }
}
