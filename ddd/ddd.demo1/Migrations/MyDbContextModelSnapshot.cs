﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ddd.demo1.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Shops", (string)null);
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("passwordHash")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Blog", b =>
                {
                    b.OwnsOne("MultiLangString", "Body", b1 =>
                        {
                            b1.Property<int>("BlogId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Chinese")
                                .HasMaxLength(255)
                                .IsUnicode(true)
                                .HasColumnType("TEXT");

                            b1.Property<string>("English")
                                .HasMaxLength(255)
                                .IsUnicode(false)
                                .HasColumnType("TEXT");

                            b1.HasKey("BlogId");

                            b1.ToTable("Blogs");

                            b1.WithOwner()
                                .HasForeignKey("BlogId");
                        });

                    b.OwnsOne("MultiLangString", "Title", b1 =>
                        {
                            b1.Property<int>("BlogId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Chinese")
                                .HasMaxLength(255)
                                .IsUnicode(true)
                                .HasColumnType("TEXT");

                            b1.Property<string>("English")
                                .HasMaxLength(255)
                                .IsUnicode(false)
                                .HasColumnType("TEXT");

                            b1.HasKey("BlogId");

                            b1.ToTable("Blogs");

                            b1.WithOwner()
                                .HasForeignKey("BlogId");
                        });

                    b.Navigation("Body")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Shop", b =>
                {
                    b.OwnsOne("Geo", "Location", b1 =>
                        {
                            b1.Property<int>("ShopId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("Latitude")
                                .HasColumnType("REAL");

                            b1.Property<double>("Longitude")
                                .HasColumnType("REAL");

                            b1.HasKey("ShopId");

                            b1.ToTable("Shops");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
