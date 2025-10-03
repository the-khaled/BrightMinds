
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
    internal class CartItemConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            //builder.Property(c => c.BookId).IsRequired();
            builder.Property(c=>c.Price).IsRequired();
            builder.Property(B => B.Price).HasColumnType("decimal(18,2)");

            builder.Property(c=>c.CartId).IsRequired();
            //builder.Property(c=>c.Quantity).IsRequired();
            builder.HasOne(B => B.Cart).WithMany(B => B.Items).HasForeignKey(B => B.CartId);

        }
    }
}
