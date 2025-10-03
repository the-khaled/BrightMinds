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
    internal class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Ignore(i => i.Id);
            builder.Property(i => i.JobTitle).IsRequired(false);
            builder.Property(i => i.Qualifications).IsRequired(false);
            builder.HasMany(i=>i.Courses).WithOne(c=>c.Instructor)
                .HasForeignKey(i=>i.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
