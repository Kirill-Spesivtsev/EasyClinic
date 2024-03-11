﻿// <auto-generated />
using EasyClinic.OfficesService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyClinic.OfficesService.Infrastructure.Migrations
{
    [DbContext(typeof(OfficesServiceDbContext))]
    partial class OfficesServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EasyClinic.OfficesService.Domain.Entities.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("integer");

                    b.Property<string>("OfficeNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PhotoBlob")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RegistryPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });
#pragma warning restore 612, 618
        }
    }
}
