﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project1.DBContext;

#nullable disable

namespace Project1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220624131127_mig1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project1.Department", b =>
                {
                    b.Property<int>("departmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("departmentId"), 1L, 1);

                    b.Property<string>("departmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("departmentId");

                    b.ToTable("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
