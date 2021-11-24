using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubjectsAndDistrictsDbContext.Model.Configuration
{
    class DistrictDbDTOConfiguration : IEntityTypeConfiguration<DistrictDbDTO>
    {
        public void Configure(EntityTypeBuilder<DistrictDbDTO> builder)
        {
            builder
                .HasKey("Code");

            builder
                .Property(p => p.Code)
                .HasColumnName("code")
                .HasColumnType("bigint")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}
