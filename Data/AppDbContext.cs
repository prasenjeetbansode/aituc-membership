using Microsoft.EntityFrameworkCore;
using AITUC.Models;


namespace AITUC.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=membership.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Membership>()
                .HasOne(m => m.Member)
                .WithMany(m => m.Memberships)
                .HasForeignKey(m => m.MemberID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
