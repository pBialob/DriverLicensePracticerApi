﻿// <auto-generated />
using DriverLicensePracticerApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DriverLicensePracticerApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnswerA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoriesToSet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SafetyExplanation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.QuestionCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionCategories");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.QuestionCategory", b =>
                {
                    b.HasOne("DriverLicensePracticerApi.Entities.Category", "Category")
                        .WithMany("QuestionCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DriverLicensePracticerApi.Entities.Question", "Question")
                        .WithMany("QuestionCategories")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.User", b =>
                {
                    b.HasOne("DriverLicensePracticerApi.Entities.Role", "role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.Category", b =>
                {
                    b.Navigation("QuestionCategories");
                });

            modelBuilder.Entity("DriverLicensePracticerApi.Entities.Question", b =>
                {
                    b.Navigation("QuestionCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
