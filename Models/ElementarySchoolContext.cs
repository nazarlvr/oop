using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ElemSchool
{
    public partial class ElementarySchoolContext : DbContext
    {
        public ElementarySchoolContext()
        {
        }

        public ElementarySchoolContext(DbContextOptions<ElementarySchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ElementarySchool> ElementarySchools { get; set; }
        public virtual DbSet<MedicalCard> MedicalCards { get; set; }
        public virtual DbSet<Pupil> Pupils { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-HUNRDISTUA0\\MSSQLSERVER02; Database=ElementarySchool; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subjects)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElementarySchoolId");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecializationId");
            });

            modelBuilder.Entity<ElementarySchool>(entity =>
            {
                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SchoolTypeId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MedicalCard>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Pupil>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassId");

                entity.HasOne(d => d.MedicalCard)
                    .WithMany(p => p.Pupils)
                    .HasForeignKey(d => d.MedicalCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicalCardID");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryId");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassTeacherId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
