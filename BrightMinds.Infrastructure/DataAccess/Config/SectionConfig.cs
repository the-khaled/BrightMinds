using BrightMinds.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.DataAccess.Config
{
    internal class SectionConfig : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(s => s.Order).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s=>s.Description).IsRequired(false);
            builder.HasOne(s=>s.Course).WithMany(c => c.Sections)
                .HasForeignKey(s=>s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
