using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<PayMethod> PayMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<TransportMethod> TransportMethods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=THUHUYEN;Database=KidsFashionDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransportMethod>()
            .ToTable("TRANSPORT_METHOD")
            .HasKey(x => x.TransportMethodid);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.TransportMethod)
            .WithMany()
            .HasForeignKey(o => o.TransportMethodid);
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADMINS__3214EC274C042050");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Role).HasDefaultValue("Admin");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CATEGORY__A50F9896210394B9");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_CATEGORY_PARENT");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Colorid).HasName("PK__COLOR__6FDDF3C465CC4824");

            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Contactid).HasName("PK__CONTACT__79911868EA517AC6");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRead).HasDefaultValue((byte)0);
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("PK__CUSTOMER__61DBD7885F7E911F");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
            entity.Property(e => e.UserType).HasDefaultValue("CUSTOMER");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Materialid).HasName("PK__MATERIAL__278B51D527E482C7");

            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Ordersid).HasName("PK__ORDERS__B0B1A3EBEB50343D");

            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
            entity.Property(e => e.OrdersDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDERS_CUSTOMER");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.OrdersDetailsid).HasName("PK__ORDERS_D__E795C3F15F283861");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Orders).WithMany(p => p.OrdersDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OD_ORDERS");

            entity.HasOne(d => d.Productvariant).WithMany(p => p.OrdersDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OD_VARIANT");
        });

        modelBuilder.Entity<PayMethod>(entity =>
        {
            entity.HasKey(e => e.PayMethodid).HasName("PK__PAY_METH__55A067C94A097C18");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCT__34980AA2D4885042");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
            entity.Property(e => e.Price).HasDefaultValue(0m);

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_PRODUCT_CATEGORY");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImagesid).HasName("PK__PRODUCT___BE142DD3D08164A2");

            entity.Property(e => e.Isdefault).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Color).WithMany(p => p.ProductImages).HasConstraintName("FK_PI_COLOR");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PI_PRODUCT");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.ProductVariantid).HasName("PK__PRODUCT___1342E27527D2FC8E");

            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Quantity).HasDefaultValue(0);

            entity.HasOne(d => d.Color).WithMany(p => p.ProductVariants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PV_COLOR");

            entity.HasOne(d => d.Material).WithMany(p => p.ProductVariants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PV_MATERIAL");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PV_PRODUCT");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductVariants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PV_SIZE");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Reviewid).HasName("PK__REVIEW__DDDCEB4A57C3AC9B");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsApproved).HasDefaultValue((byte)0);
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews).HasConstraintName("FK_REVIEW_CUSTOMER");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVIEW_PRODUCT");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Sizeid).HasName("PK__SIZE__903CA348CE388A5F");

            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<TransportMethod>(entity =>
        {
            entity.HasKey(e => e.TransportMethodid).HasName("PK__TRANSPOR__15CD30836FAFC2D8");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Isactive).HasDefaultValue((byte)1);
            entity.Property(e => e.Isdelete).HasDefaultValue((byte)0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
