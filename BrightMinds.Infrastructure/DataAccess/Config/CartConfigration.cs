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
    public class CartConfigration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
           builder.Property(c=>c.UserId).IsRequired();
           builder.Property(c=>c.CreationDate).IsRequired();
        }
    }
}
