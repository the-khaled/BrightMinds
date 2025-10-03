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
    internal class VideoConfig : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.Property(v => v.Description).IsRequired(false);
            builder.Property(v => v.Name).IsRequired();
            builder.Property(v => v.VideoUrl).IsRequired();
            builder.Property(v => v.Duration).IsRequired();
            builder.Property(v => v.CoverUrl).IsRequired();
            builder.Property(v => v.VideoUrl).IsRequired();
            builder.HasOne(v => v.Section).WithMany(s => s.Videos)
                .HasForeignKey(v => v.SectionId);

        }
    }
}
