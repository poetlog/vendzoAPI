﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vendzoAPI.Models;

#nullable disable

namespace vendzoAPI.Migrations
{
    [DbContext(typeof(VendzoContext))]
    partial class VendzoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

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

            modelBuilder.Entity("vendzoAPI.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Basket", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

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
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("varchar(75)")
                        .HasColumnName("category");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

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
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("BillAddress")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("BillAddress");

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

            modelBuilder.Entity("vendzoAPI.Models.OrderEntry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("buyerId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("itemId");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("orderId");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("photo");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("sellerId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SellerId");

                    b.ToTable("orderEntry", (string)null);
                });

            modelBuilder.Entity("vendzoAPI.Models.Promotion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric(8, 2)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTimeOffset?>("Expires")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("expires");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("createdAt")
                        .HasDefaultValueSql("GETDATE()");

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

                    b.Property<bool>("IsClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isClient");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("LoginId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("loginId");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("vendzoAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("vendzoAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vendzoAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("vendzoAPI.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("vendzoAPI.Models.OrderEntry", b =>
                {
                    b.HasOne("vendzoAPI.Models.User", "Buyer")
                        .WithMany("BuyerOrderEntries")
                        .HasForeignKey("BuyerId")
                        .IsRequired()
                        .HasConstraintName("FK_orderEntry_Buyer");

                    b.HasOne("vendzoAPI.Models.Item", "Item")
                        .WithMany("OrderEntries")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK_orderEntry_Item");

                    b.HasOne("vendzoAPI.Models.Order", "Order")
                        .WithMany("OrderEntries")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_orderEntry_Order");

                    b.HasOne("vendzoAPI.Models.User", "Seller")
                        .WithMany("SellerOrderEntries")
                        .HasForeignKey("SellerId")
                        .IsRequired()
                        .HasConstraintName("FK_orderEntry_Seller");

                    b.Navigation("Buyer");

                    b.Navigation("Item");

                    b.Navigation("Order");

                    b.Navigation("Seller");
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

                    b.Navigation("OrderEntries");
                });

            modelBuilder.Entity("vendzoAPI.Models.Order", b =>
                {
                    b.Navigation("OrderEntries");
                });

            modelBuilder.Entity("vendzoAPI.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Baskets");

                    b.Navigation("BuyerOrderEntries");

                    b.Navigation("Items");

                    b.Navigation("Orders");

                    b.Navigation("SellerOrderEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
