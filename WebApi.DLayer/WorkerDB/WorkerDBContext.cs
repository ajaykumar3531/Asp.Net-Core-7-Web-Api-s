using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.DLayer.WorkerDB
{
    public partial class WorkerDBContext : DbContext
    {
        public WorkerDBContext()
        {
        }

        public WorkerDBContext(DbContextOptions<WorkerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bonu> Bonus { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=AJAYKUMAR\\SQLEXPRESS;Database=WorkerDB;user id=sa;password=3531;encrypt=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bonu>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BonusAmount).HasColumnName("BONUS_AMOUNT");

                entity.Property(e => e.BonusDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BONUS_DATE");

                entity.Property(e => e.WorkerRefId).HasColumnName("WORKER_REF_ID");

                entity.HasOne(d => d.WorkerRef)
                    .WithMany()
                    .HasForeignKey(d => d.WorkerRefId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Bonus__WORKER_RE__5BE2A6F2");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Title");

                entity.Property(e => e.AffectedFrom)
                    .HasColumnType("datetime")
                    .HasColumnName("AFFECTED_FROM");

                entity.Property(e => e.WorkerRefId).HasColumnName("WORKER_REF_ID");

                entity.Property(e => e.WorkerTitle)
                    .HasMaxLength(25)
                    .HasColumnName("WORKER_TITLE");

                entity.HasOne(d => d.WorkerRef)
                    .WithMany()
                    .HasForeignKey(d => d.WorkerRefId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Title__WORKER_RE__5DCAEF64");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");

                entity.Property(e => e.WorkerId).HasColumnName("WORKER_ID");

                entity.Property(e => e.Department)
                    .HasMaxLength(25)
                    .HasColumnName("DEPARTMENT");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JOINING_DATE");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Salary).HasColumnName("SALARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
