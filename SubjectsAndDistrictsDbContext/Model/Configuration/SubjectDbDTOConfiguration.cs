using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubjectsAndDistrictsDbContext.Model.Configuration
{
    class SubjectDbDTOConfiguration : IEntityTypeConfiguration<SubjectDbDTO>
    {
        public void Configure(EntityTypeBuilder<SubjectDbDTO> builder)
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

            builder
                .Property(p => p.AdminCenterName)
                .HasColumnName("admin_center_name")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(p => p.Population)
                .HasColumnName("population")
                .HasColumnType("float(53)")
                .IsRequired();

            builder
                .Property(p => p.Square)
                .HasColumnName("square")
                .HasColumnType("float(53)")
                .IsRequired();

            builder
                .HasOne(s => s.District)
                .WithMany(d => d.Subjects)
                .HasForeignKey("district_code")
                .IsRequired();
        }
    }
}
