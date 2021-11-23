using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SNDDbContext.ModelDB
{
    public partial class DistrictsAndSubjectsDBContext : DbContext
    {
        public DistrictsAndSubjectsDBContext()
        {
        }

        public DistrictsAndSubjectsDBContext(DbContextOptions<DistrictsAndSubjectsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<VwFilterSubjectsByDistrictCode> VwFilterSubjectsByDistrictCodes { get; set; }
        public virtual DbSet<VwFilterSubjectsByName> VwFilterSubjectsByNames { get; set; }
        public virtual DbSet<VwSearchDistrictByCode> VwSearchDistrictByCodes { get; set; }
        public virtual DbSet<VwSearchDistrictByName> VwSearchDistrictByNames { get; set; }
        public virtual DbSet<VwSearchSubjectByName> VwSearchSubjectByNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__District__A25C5AA667CE29AF");

                entity.ToTable("District");

                entity.Property(e => e.Code).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Subject__A25C5AA690DA8284");

                entity.ToTable("Subject");

                entity.Property(e => e.Code).ValueGeneratedNever();

                entity.Property(e => e.AdminCenterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DistrictNavigation)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.District)
                    .HasConstraintName("FK__Subject__Distric__267ABA7A");
            });

            modelBuilder.Entity<VwFilterSubjectsByDistrictCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwFilterSubjectsByDistrictCode");

                entity.Property(e => e.AdminCenterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwFilterSubjectsByName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwFilterSubjectsByName");

                entity.Property(e => e.AdminCenterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwSearchDistrictByCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchDistrictByCode");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwSearchDistrictByName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchDistrictByName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwSearchSubjectByName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchSubjectByName");

                entity.Property(e => e.AdminCenterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
