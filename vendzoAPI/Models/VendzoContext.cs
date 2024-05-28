using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace vendzoAPI.Models;

public partial class VendzoContext : DbContext
{
    public VendzoContext()
    {
    }

    public VendzoContext(DbContextOptions<VendzoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<OrderEntry> OrderEntries { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=vendzo;Trusted_Connection=True;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3213E83F29F4F205");

            entity.ToTable("Address");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("contactNo");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("userId");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Address__userId__4CA06362");
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Basket__3213E83FDDDA08BF");

            entity.ToTable("Basket");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ItemId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("itemId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("userId");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Item).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Basket__itemId__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Basket__userId__5629CD9C");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3213E83F2E15E9F0");

            entity.ToTable("Item");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Photo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SellerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("sellerId");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Title)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()");

            entity.HasOne(d => d.Seller).WithMany(p => p.Items)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__sellerId__4F7CD00D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83FCC5800CF");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DeliverDate).HasColumnName("deliverDate");
            entity.Property(e => e.OrderDate).HasColumnName("orderDate");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("shipAddress");
            entity.Property(e => e.BillAddress)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BillAddress");
            entity.Property(e => e.ShipDate).HasColumnName("shipDate");
            entity.Property(e => e.Status)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("numeric(9, 2)")
                .HasColumnName("total");
            entity.Property(e => e.TrackingNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("trackingNo");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Order__userId__5AEE82B9");

            /*entity.HasOne(d => d.Seller)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK_Orders_Seller");*/

            entity.HasMany(d => d.OrderEntries)
                .WithOne(p => p.Order)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Orders_OrderEntries");
        });

        modelBuilder.Entity<OrderEntry>(entity =>
        {
            entity.ToTable("orderEntry");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");

            entity.Property(e => e.OrderId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("orderId");

            entity.Property(e => e.BuyerId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("buyerId");

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("itemId");

            entity.Property(e => e.SellerId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("sellerId");

            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .IsRequired()
                .HasColumnName("price");

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("createdAt");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderEntries)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderEntry_Order");

            entity.HasOne(d => d.Buyer)
                .WithMany(p => p.BuyerOrderEntries)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderEntry_Buyer");

            entity.HasOne(d => d.Seller)
                .WithMany(p => p.SellerOrderEntries)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderEntry_Seller");

            entity.HasOne(d => d.Item)
                .WithMany(p => p.OrderEntries)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderEntry_Item");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3213E83F535209CC");

            entity.ToTable("Promotion");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Expires).HasColumnName("expires");
            entity.Property(e => e.PromoCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("promoCode");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83FD4F6D364");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("contactNo");
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("currentAddress");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.IsClient)
                .HasColumnName("isClient")
                .HasDefaultValue(false);
            entity.Property(e => e.IsDeleted)
                .HasColumnName("IsDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.CurrentAddressNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.CurrentAddress)
                .HasConstraintName("FK_currentAddress");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
