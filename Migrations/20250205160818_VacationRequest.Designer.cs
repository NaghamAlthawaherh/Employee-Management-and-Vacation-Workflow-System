﻿// <auto-generated />
using System;
using Employee_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Employee_Management.Migrations
{
    [DbContext(typeof(Employee_DbContext))]
    [Migration("20250205160818_VacationRequest")]
    partial class VacationRequest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Employee_Management.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Employee_Management.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeNumber")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<string>("ReportsTo")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<decimal>("Salary")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("VacationDaysLeft")
                        .HasColumnType("int");

                    b.HasKey("EmployeeNumber");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("ReportsTo");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Employee_Management.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"));

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Employee_Management.Models.RequestState", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"));

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("StateId");

                    b.ToTable("RequestStates");
                });

            modelBuilder.Entity("Employee_Management.Models.VacationRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("DeclinedBy")
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("date");

                    b.Property<int?>("RequestState_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("date");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Total_Days")
                        .HasColumnType("int");

                    b.Property<string>("VacationType_Code")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("RequestId");

                    b.HasIndex("ApprovedBy");

                    b.HasIndex("DeclinedBy");

                    b.HasIndex("EmployeeNumber");

                    b.HasIndex("RequestState_Id");

                    b.HasIndex("VacationType_Code");

                    b.ToTable("VacationRequests");
                });

            modelBuilder.Entity("Employee_Management.Models.VacationType", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("VacationTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Code");

                    b.ToTable("VacationTypes");
                });

            modelBuilder.Entity("Employee_Management.Models.Employee", b =>
                {
                    b.HasOne("Employee_Management.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Employee_Management.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Employee_Management.Models.Employee", "Manager")
                        .WithMany("DirectReports")
                        .HasForeignKey("ReportsTo")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");

                    b.Navigation("Manager");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Employee_Management.Models.VacationRequest", b =>
                {
                    b.HasOne("Employee_Management.Models.Employee", "Approver")
                        .WithMany()
                        .HasForeignKey("ApprovedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Employee_Management.Models.Employee", "Decliner")
                        .WithMany()
                        .HasForeignKey("DeclinedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Employee_Management.Models.Employee", "Employee")
                        .WithMany("VacationRequests")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Employee_Management.Models.RequestState", "RequestState")
                        .WithMany("VacationRequests")
                        .HasForeignKey("RequestState_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Employee_Management.Models.VacationType", "VacationType")
                        .WithMany("VacationRequests")
                        .HasForeignKey("VacationType_Code")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Decliner");

                    b.Navigation("Employee");

                    b.Navigation("RequestState");

                    b.Navigation("VacationType");
                });

            modelBuilder.Entity("Employee_Management.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Employee_Management.Models.Employee", b =>
                {
                    b.Navigation("DirectReports");

                    b.Navigation("VacationRequests");
                });

            modelBuilder.Entity("Employee_Management.Models.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Employee_Management.Models.RequestState", b =>
                {
                    b.Navigation("VacationRequests");
                });

            modelBuilder.Entity("Employee_Management.Models.VacationType", b =>
                {
                    b.Navigation("VacationRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
