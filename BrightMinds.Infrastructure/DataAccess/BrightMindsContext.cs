
using BrightMinds.Core.Models;
using Library.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.DataAccess
{
    public class BrightMindsContext:IdentityDbContext   <AppUser>
    {

        public BrightMindsContext(DbContextOptions<BrightMindsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
              .Property(u => u.WaletBalance)
              .HasPrecision(18, 2);  // Precision = 18, Scale = 2

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Video>()
                .Property(v => v.Duration)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Coupon>()
                .Property(c => c.Value)
                .HasPrecision(18, 2);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Feedback>()
           .HasOne(f => f.Course)
           .WithMany(c => c.Feedbacks)
           .HasForeignKey(f => f.CourseId);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId);
        }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructor> Instructors { get; set; }    
        public DbSet<Course> Courses { get; set; } 
        public DbSet<Section> Sections { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CouponUsage> CouponUsages { get; set; }
/*        public DbSet<AppUser> Users { get; set; }
*/
    }
}
