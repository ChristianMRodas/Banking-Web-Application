﻿// <auto-generated />
using System;
using CommerceBankOnlineBanking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommerceBankOnlineBanking.Migrations
{
    [DbContext(typeof(BankingContext))]
    partial class BankingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("CommerceBankOnlineBanking.Models.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NotifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("CommerceBankOnlineBanking.Models.NotificationRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RuleType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NotificationRule");
                });

            modelBuilder.Entity("CommerceBankOnlineBanking.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ProcessingDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Transaction");

                    b.HasData(
                        new
                        {
                            Id = new Guid("53eee79b-40bd-4abd-b36f-6e93fd6d5a64"),
                            AccountNumber = 1,
                            Action = "Account Open",
                            Amount = 0.0,
                            Balance = 5000.0,
                            Description = "Starting Balance",
                            ProcessingDate = new DateTime(2020, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("3d6b7c38-1daf-412d-beb7-04eae0aceff8")
                        });
                });

            modelBuilder.Entity("CommerceBankOnlineBanking.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5eb63d56-3a0c-48e9-bd5f-1c5370a8f695"),
                            FirstName = "Admin",
                            LastName = "Pass",
                            Password = "AMqt+XRYlYmVhyswIXpMSJfYqw3yACwWwsZl4lKWxLQTRPmEGut9g9AqG2J2B4AK+Q==",
                            UserName = "AdminPass"
                        },
                        new
                        {
                            Id = new Guid("3d6b7c38-1daf-412d-beb7-04eae0aceff8"),
                            FirstName = "Test",
                            LastName = "User",
                            Password = "AKATIz42cBo4tGpfyKkknWwiRQgN82ITGcGTrAxxbktqYX1pPChd3rm4BtzjlKzw3w==",
                            UserName = "TestUser"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
