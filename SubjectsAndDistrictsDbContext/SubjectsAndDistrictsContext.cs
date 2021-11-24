using Microsoft.EntityFrameworkCore;
using SubjectsAndDistrictsDbContext.Connection;
using SubjectsAndDistrictsDbContext.Model.Configuration;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SubjectsAndDistrictsDbContext
{
    class SubjectsAndDistrictsContext : DbContext
    {
        public SubjectsAndDistrictsContext() : base() { }

        public SubjectsAndDistrictsContext(DbContextOptions options) : base(options) { }

        public DbSet<SubjectDbDTO> Subjects { get; set; }
        public DbSet<DistrictDbDTO> Districts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringConfig().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DistrictDbDTOConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectDbDTOConfiguration());
        }
    }
}
