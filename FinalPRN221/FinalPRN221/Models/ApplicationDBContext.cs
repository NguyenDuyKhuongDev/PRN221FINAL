using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalPRN221.Models;

public partial class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<PayRoll> PayRolls { get; set; }

    public virtual DbSet<PayrollStatus> PayrollStatuses { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER03;Database=FinalPRN221;User Id=sa;Password=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.AbsentReasonId).HasColumnName("AbsentReasonID");
            entity.Property(e => e.CheckInTime).HasPrecision(0);
            entity.Property(e => e.CheckOutTime).HasPrecision(0);
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(450)
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.OverTimeHours).HasColumnType("decimal(2, 2)");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.WorkHours).HasColumnType("decimal(2, 2)");

            entity.HasOne(d => d.Status).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_AttendanceStatus");
        });

        modelBuilder.Entity<AttendanceStatus>(entity =>
        {
            entity.ToTable("AttendanceStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PayRoll>(entity =>
        {
            entity.ToTable("PayRoll");

            entity.Property(e => e.PayrollId).HasColumnName("PayrollID");
            entity.Property(e => e.Allowance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonus).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(450)
                .HasColumnName("EmployeeID");
            entity.Property(e => e.FineSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.OverTimePay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UpdateBy).HasMaxLength(100);

            entity.HasOne(d => d.Status).WithMany(p => p.PayRolls)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PayRoll_PayrollStatus");
        });

        modelBuilder.Entity<PayrollStatus>(entity =>
        {
            entity.ToTable("PayrollStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        // OnModelCreatingPartial(modelBuilder);
    }

    //  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}