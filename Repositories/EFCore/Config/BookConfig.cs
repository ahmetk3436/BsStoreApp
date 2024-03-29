﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(b => b.Price)
                   .IsRequired()
                   ;

            builder.HasData(
                new Book { Id = 1, Title = "Karagöz Ve Hacivat", Price = 75 },
                 new Book { Id = 2, Title = "Mesnevi", Price = 175 },
                  new Book { Id = 3, Title = "Devlet", Price = 375 }
                );
        }
    }
}
