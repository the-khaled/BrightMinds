using BrightMinds.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.DataAccess.Config
{
    internal class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.PictureUrl).IsRequired();
            builder.HasMany(c => c.Sections).WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(c=>c.Category).WithMany(c=>c.Courses)
                .HasForeignKey(c=>c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
          


        }
    }
}
