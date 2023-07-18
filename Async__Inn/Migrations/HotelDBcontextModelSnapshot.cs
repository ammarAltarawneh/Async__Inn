﻿// <auto-generated />
using Async__Inn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Async__Inn.Migrations
{
    [DbContext(typeof(AsyncInnDbContext))]
    partial class HotelDBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Async__Inn.Models.Amenity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "fitness center"
                        },
                        new
                        {
                            ID = 2,
                            Name = "swiming pool"
                        },
                        new
                        {
                            ID = 3,
                            Name = "buisiness center"
                        });
                });

            modelBuilder.Entity("Async__Inn.Models.Hotel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            City = "Irbid",
                            Country = "Jordan",
                            Name = "SahNoom",
                            Phone = "0775555555",
                            State = "Alhimah",
                            StreetAdress = "st60"
                        },
                        new
                        {
                            ID = 2,
                            City = "Irbid",
                            Country = "Jordan",
                            Name = "Castle",
                            Phone = "0781111111",
                            State = "Hakama",
                            StreetAdress = "st30"
                        },
                        new
                        {
                            ID = 3,
                            City = "Irbid",
                            Country = "Jordan",
                            Name = "Diamond",
                            Phone = "0792222222",
                            State = "Howara",
                            StreetAdress = "st100"
                        });
                });

            modelBuilder.Entity("Async__Inn.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Layout = 20,
                            Name = "Bedroom"
                        },
                        new
                        {
                            ID = 2,
                            Layout = 30,
                            Name = "livingroom"
                        },
                        new
                        {
                            ID = 3,
                            Layout = 10,
                            Name = "Kitchen"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
