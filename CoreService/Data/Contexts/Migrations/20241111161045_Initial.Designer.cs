﻿// <auto-generated />
using System;
using CoreService.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreService.Data.Contexts.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20241111161045_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("core")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreService.Data.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Admin", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Calendar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ClosingTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OpeningTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("WorkDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Calendar", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BoomCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Customer", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Employee", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Food", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<double>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0);

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Ingredient", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Location", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.RequiredIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RequiredIngredient", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Restaurant", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Shipper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Shipper", "core");
                });

            modelBuilder.Entity("CoreService.Data.Models.Calendar", b =>
                {
                    b.HasOne("CoreService.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Calendars")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("CoreService.Data.Models.Employee", b =>
                {
                    b.HasOne("CoreService.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Employees")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("CoreService.Data.Models.Food", b =>
                {
                    b.HasOne("CoreService.Data.Models.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreService.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Foods")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("CoreService.Data.Models.Ingredient", b =>
                {
                    b.HasOne("CoreService.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Ingredients")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("CoreService.Data.Models.Location", b =>
                {
                    b.HasOne("CoreService.Data.Models.Customer", "Customer")
                        .WithMany("Locations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CoreService.Data.Models.RequiredIngredient", b =>
                {
                    b.HasOne("CoreService.Data.Models.Food", "Food")
                        .WithMany("RequiredIngredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreService.Data.Models.Ingredient", "Ingredient")
                        .WithMany("RequiredIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CoreService.Data.Models.Category", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("CoreService.Data.Models.Customer", b =>
                {
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("CoreService.Data.Models.Food", b =>
                {
                    b.Navigation("RequiredIngredients");
                });

            modelBuilder.Entity("CoreService.Data.Models.Ingredient", b =>
                {
                    b.Navigation("RequiredIngredients");
                });

            modelBuilder.Entity("CoreService.Data.Models.Restaurant", b =>
                {
                    b.Navigation("Calendars");

                    b.Navigation("Employees");

                    b.Navigation("Foods");

                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
