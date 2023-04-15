using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BibrusServer.Models;

public partial class BibrusDbContext : DbContext
{
    public BibrusDbContext(DbContextOptions<BibrusDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Attandance> Attandances { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0798561772");

            entity.ToTable("Address");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.HomeNumber).HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.PostalCode).HasMaxLength(6);
            entity.Property(e => e.Road).HasMaxLength(100);
            entity.Property(e => e.Voivoidship).HasMaxLength(100);
        });

        modelBuilder.Entity<Attandance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attandan__3214EC072A56079D");

            entity.ToTable("Attandance");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Value).HasMaxLength(3);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attandances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Attandance_ToEmployee");

            entity.HasOne(d => d.Student).WithMany(p => p.Attandances)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Attandance_ToStudent");

            entity.HasOne(d => d.Subject).WithMany(p => p.Attandances)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Attandance_ToSubject");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3214EC07AD70D831");

            entity.ToTable("Class");

            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.Year).HasMaxLength(10);

            entity.HasOne(d => d.Employee).WithMany(p => p.Classes)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Class_ToEmployee");

            entity.HasOne(d => d.Subject).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Class_ToSubject");
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Credenti__3214EC07A8DD8803");

            entity.Property(e => e.Login).HasMaxLength(16);
            entity.Property(e => e.Password).HasMaxLength(64);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0755BC9878");

            entity.ToTable("Employee");

            entity.Property(e => e.Role).HasMaxLength(3);

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Employee_ToEmployee");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grade__3214EC079A9452CB");

            entity.ToTable("Grade");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Grade_ToStudent");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.Subjectid)
                .HasConstraintName("FK_Grade_ToSubject");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__School__3214EC07BFBD2EB5");

            entity.ToTable("School");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Address).WithMany(p => p.Schools)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_School_ToAddress");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC079748208C");

            entity.ToTable("Student");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Student_ToClass");

            entity.HasOne(d => d.ParentUser).WithMany(p => p.StudentParentUsers)
                .HasForeignKey(d => d.ParentUserId)
                .HasConstraintName("FK_StudentParent_ToUser");

            entity.HasOne(d => d.User).WithMany(p => p.StudentUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Student_ToUser");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subject__3214EC07BDE3C8AB");

            entity.ToTable("Subject");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Subject_ToEmployee");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07A43E0B57");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Type)
                .HasMaxLength(3)
                .HasColumnName("type");

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_User_ToAddress");

            entity.HasOne(d => d.Credentials).WithMany(p => p.Users)
                .HasForeignKey(d => d.CredentialsId)
                .HasConstraintName("FK_User_ToCredentials");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
