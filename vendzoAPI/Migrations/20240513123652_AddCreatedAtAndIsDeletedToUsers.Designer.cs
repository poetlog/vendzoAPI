﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vendzoAPI.Models;

#nullable disable

namespace vendzoAPI.Migrations
{
    [DbContext(typeof(VendzoContext))]
    [Migration("20240513123652_AddCreatedAtAndIsDeletedToUsers")]
    partial class AddCreatedAtAndIsDeletedToUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("vendzoAPI.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("Address1")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("address");

                    b.Property<string>("ContactNo")
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("contactNo");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("userId");

                    b.HasKey("Id")
                        .HasName("PK__Address__3213E83F29F4F205");

                    b.HasIndex("UserId");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Basket", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("ItemId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("itemId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("userId");

                    b.HasKey("Id")
                        .HasName("PK__Basket__3213E83FDDDA08BF");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Basket", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("varchar(75)")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("Photo")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("photo");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric(8, 2)")
                        .HasColumnName("price");

                    b.Property<string>("SellerId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("sellerId");

                    b.Property<int?>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<string>("Title")
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("varchar(75)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("PK__Item__3213E83F2E15E9F0");

                    b.HasIndex("SellerId");

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset?>("DeliverDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("deliverDate");

                    b.Property<DateTimeOffset?>("OrderDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("orderDate");

                    b.Property<string>("ShipAddress")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("shipAddress");

                    b.Property<DateTimeOffset?>("ShipDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("shipDate");

                    b.Property<string>("Status")
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("status");

                    b.Property<decimal?>("Total")
                        .HasColumnType("numeric(9, 2)")
                        .HasColumnName("total");

                    b.Property<string>("TrackingNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("trackingNo");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("userId");

                    b.HasKey("Id")
                        .HasName("PK__Order__3213E83FCC5800CF");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Promotion", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric(8, 2)")
                        .HasColumnName("amount");

                    b.Property<DateTimeOffset?>("Expires")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("expires");

                    b.Property<string>("PromoCode")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("promoCode");

                    b.Property<string>("Type")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("PK__Promotio__3213E83F535209CC");

                    b.ToTable("Promotion", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("ContactNo")
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("contactNo");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentAddress")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("currentAddress");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Pass")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("pass");

                    b.Property<string>("UserType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("userType");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("PK__User__3213E83FD4F6D364");

                    b.HasIndex("CurrentAddress");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Address", b =>
                {
                    b.HasOne("vendzoAPI.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Address__userId__4CA06362");

                    b.Navigation("User");
                });

            modelBuilder.Entity("vendzoAPI.Models.Basket", b =>
                {
                    b.HasOne("vendzoAPI.Models.Item", "Item")
                        .WithMany("Baskets")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("FK__Basket__itemId__571DF1D5");

                    b.HasOne("vendzoAPI.Models.User", "User")
                        .WithMany("Baskets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Basket__userId__5629CD9C");

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("vendzoAPI.Models.Item", b =>
                {
                    b.HasOne("vendzoAPI.Models.User", "Seller")
                        .WithMany("Items")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Item__sellerId__4F7CD00D");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("vendzoAPI.Models.Order", b =>
                {
                    b.HasOne("vendzoAPI.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Order__userId__5AEE82B9");

                    b.Navigation("User");
                });

            modelBuilder.Entity("vendzoAPI.Models.User", b =>
                {
                    b.HasOne("vendzoAPI.Models.Address", "CurrentAddressNavigation")
                        .WithMany("Users")
                        .HasForeignKey("CurrentAddress")
                        .HasConstraintName("FK_currentAddress");

                    b.Navigation("CurrentAddressNavigation");
                });

            modelBuilder.Entity("vendzoAPI.Models.Address", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("vendzoAPI.Models.Item", b =>
                {
                    b.Navigation("Baskets");
                });

            modelBuilder.Entity("vendzoAPI.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Baskets");

                    b.Navigation("Items");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
