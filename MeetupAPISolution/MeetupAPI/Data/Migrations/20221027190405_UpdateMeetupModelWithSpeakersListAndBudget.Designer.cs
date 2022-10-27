﻿// <auto-generated />
using System;
using MeetupAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetupAPI.Data.Migrations
{
    [DbContext(typeof(MeetupAPIDbContext))]
    [Migration("20221027190405_UpdateMeetupModelWithSpeakersListAndBudget")]
    partial class UpdateMeetupModelWithSpeakersListAndBudget
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MeetupAPI.Models.MeetupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Budget")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sponsor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MeetupModels");
                });

            modelBuilder.Entity("MeetupAPI.Models.SpeakerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeetupModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetupModelId");

                    b.ToTable("SpeakerModel");
                });

            modelBuilder.Entity("MeetupAPI.Models.SpeakerModel", b =>
                {
                    b.HasOne("MeetupAPI.Models.MeetupModel", "MeetupModel")
                        .WithMany("Speakers")
                        .HasForeignKey("MeetupModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetupModel");
                });

            modelBuilder.Entity("MeetupAPI.Models.MeetupModel", b =>
                {
                    b.Navigation("Speakers");
                });
#pragma warning restore 612, 618
        }
    }
}
