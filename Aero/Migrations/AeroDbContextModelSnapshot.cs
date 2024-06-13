﻿// <auto-generated />
using System;
using Aero.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aero.Migrations
{
    [DbContext(typeof(AeroDbContext))]
    partial class AeroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Aero.Database.Models.AirlineDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IATACode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FullName", "IATACode")
                        .IsUnique();

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("Aero.Database.Models.FlightDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AirlineId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("FlightNumber")
                        .IsUnique();

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Aero.Database.Models.FlightDb", b =>
                {
                    b.HasOne("Aero.Database.Models.AirlineDb", "Airline")
                        .WithMany()
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");
                });
#pragma warning restore 612, 618
        }
    }
}