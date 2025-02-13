

using Employee_Management.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Employee_Management;

public class Employee_DbContext : DbContext
{

    public Employee_DbContext()
    {
    }

    public Employee_DbContext(DbContextOptions<Employee_DbContext> options)

            : base(options)
    {
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<VacationType>VacationTypes{ get; set; }
    public DbSet<RequestState>RequestStates{ get; set; }
    public DbSet<VacationRequest>VacationRequests{ get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-51BRH30;Database=[Employee Management];Trusted_Connection=True;TrustServerCertificate=True;");
    
    var builder = WebApplication.CreateBuilder();

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Department Entity Class
        modelBuilder.Entity<Department>()
        .HasKey(d => d.DepartmentId);
        modelBuilder.Entity<Department>()
        .Property(d => d.DepartmentName)
        .HasMaxLength(50)
        .IsRequired(true);

        // Position Entity
        modelBuilder.Entity<Position>()
        .HasKey(p => p.PositionId);
        modelBuilder.Entity<Position>()
        .Property(p => p.PositionName)
        .HasMaxLength(30)
        .IsRequired(true);

        //  Employee Entity
        modelBuilder.Entity<Employee>()
         .HasKey(e => e.EmployeeNumber);
        modelBuilder.Entity<Employee>()
         .Property(e => e.EmployeeNumber)
         .HasMaxLength(6)
         .IsRequired(true);
         modelBuilder.Entity<Employee>()
        .Property(e => e.EmployeeNumber)
        .ValueGeneratedNever(); 
        modelBuilder.Entity<Employee>()
         .Property(e=>e.EmployeeName)
         .HasMaxLength(20)
         .IsRequired(true);
        modelBuilder.Entity<Employee>()
          .Property(e=>e.Gender)
          .HasMaxLength(1)
          .IsRequired(true);
        modelBuilder.Entity<Employee>()
         .Property(e=>e.ReportsTo)
         .HasMaxLength(6)
         .IsRequired(false);
         modelBuilder.Entity<Employee>()
          .Property(e => e.Salary)
          .HasPrecision(18, 2)
          .HasDefaultValue(0);
      
         modelBuilder.Entity<Employee>()
               .HasOne(e=>e.Department)
               .WithMany(e => e.Employees)
               .HasForeignKey(e=>e.DepartmentId)
               .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Employee>()
               .HasOne(e=>e.Position)
               .WithMany(e => e.Employees)
               .HasForeignKey(e=>e.PositionId)
               .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Employee>()
        .HasOne(e=>e.Manager)
        .WithMany(e => e.DirectReports)
        .HasForeignKey(e=>e.ReportsTo)
        .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Employee>()
        .Property(e => e.VacationDaysLeft)
        .HasDefaultValue(24);
    

    // VacationType Entity
    modelBuilder.Entity<VacationType>()
    .HasKey(v=>v.Code);
    modelBuilder.Entity<VacationType>()
    .Property(v=>v.Code)
    .HasMaxLength(1);
    modelBuilder.Entity<VacationType>()
    .Property(v=>v.VacationTypeName)
    .HasMaxLength(20);

// Request State Entity
modelBuilder.Entity<RequestState>()
.HasKey(r=>r.StateId);
modelBuilder.Entity<RequestState>()
.Property(r=>r.StateName)
.HasMaxLength(10)
.IsRequired(true);

// Vacation Request Entity
 modelBuilder.Entity<VacationRequest>()
 .HasKey(vr=>vr.RequestId);
 modelBuilder.Entity<VacationRequest>()
 .Property(vr=>vr.Description)
 .HasMaxLength(100)
 .IsRequired(true);
 modelBuilder.Entity<VacationRequest>()
 .Property(vr=>vr.VacationType_Code)
 .HasMaxLength(1);
 modelBuilder.Entity<VacationRequest>()
         .Property(vr=>vr.Start_Date)
         .HasColumnType("date")
         .IsRequired(true);

          modelBuilder.Entity<VacationRequest>()
         .Property(vr=>vr.End_Date)
         .HasColumnType("date")
         .IsRequired(true);
         // Employee + VacationRequest (One-to-Many)
  modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.Employee)
            .WithMany(e => e.VacationRequests)
            .HasForeignKey(vr => vr.EmployeeNumber)
            .OnDelete(DeleteBehavior.Cascade);

// VacationRequest + RequestState (Many-to-One)
        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.RequestState)
            .WithMany(rs => rs.VacationRequests)
            .HasForeignKey(vr => vr.RequestState_Id)
            .OnDelete(DeleteBehavior.Cascade);
// VacationRequest → VacationType (Many-to-One)
        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.VacationType)
            .WithMany(vt => vt.VacationRequests)
            .HasForeignKey(vr => vr.VacationType_Code)
            .OnDelete(DeleteBehavior.Restrict);
modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.Approver)
            .WithMany()
            .HasForeignKey(vr => vr.ApprovedBy)
            .OnDelete(DeleteBehavior.NoAction);

    // VacationRequest → DeclinedByEmployee (Self-Reference)
        modelBuilder.Entity<VacationRequest>()
            .HasOne(vr => vr.Decliner)
            .WithMany()
            .HasForeignKey(vr => vr.DeclinedBy)
            .OnDelete(DeleteBehavior.NoAction);
    
    
    
 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
    
    
   
    
    
    
    
    
    
    
    
    
    }