﻿// <auto-generated />
using System;
using CoursesProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoursesProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211215180624_FirstMig")]
    partial class FirstMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoursesProject.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("int");

                    b.Property<int>("TrainerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "401d5",
                            TechnologyId = 3,
                            TrainerId = 1
                        });
                });

            modelBuilder.Entity("CoursesProject.Models.CourseStudent", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("CoursesProject.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Fadi",
                            LastName = "Hboubati"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Taghrid",
                            LastName = "Alshoqirat"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Ahmad",
                            LastName = "Ali"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Baraa",
                            LastName = "Al-Hamad"
                        });
                });

            modelBuilder.Entity("CoursesProject.Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Technologies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "python"
                        },
                        new
                        {
                            Id = 2,
                            Name = "JavaScript"
                        },
                        new
                        {
                            Id = 3,
                            Name = "CSharp"
                        });
                });

            modelBuilder.Entity("CoursesProject.Models.Trainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("attachment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ahmad",
                            attachment = "Not-Found"
                        });
                });

            modelBuilder.Entity("CoursesProject.Models.Course", b =>
                {
                    b.HasOne("CoursesProject.Models.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("CoursesProject.Models.CourseStudent", b =>
                {
                    b.HasOne("CoursesProject.Models.Course", "Course")
                        .WithMany("courseStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursesProject.Models.Student", "Student")
                        .WithMany("courseStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CoursesProject.Models.Technology", b =>
                {
                    b.HasOne("CoursesProject.Models.Course", null)
                        .WithMany("Technologies")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("CoursesProject.Models.Course", b =>
                {
                    b.Navigation("courseStudents");

                    b.Navigation("Technologies");
                });

            modelBuilder.Entity("CoursesProject.Models.Student", b =>
                {
                    b.Navigation("courseStudents");
                });
#pragma warning restore 612, 618
        }
    }
}
