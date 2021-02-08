using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using pumptally.Data.Entities;

#nullable disable

namespace pumptally.Data
{
    public partial class PumpDBContext : DbContext
    {
        public PumpDBContext()
        {
        }

        public PumpDBContext(DbContextOptions<PumpDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BiscuitCash> BiscuitCashes { get; set; }
        public virtual DbSet<Calibratation> Calibratations { get; set; }
        public virtual DbSet<Hsdmeterdip> Hsdmeterdips { get; set; }
        public virtual DbSet<Msmeterdip> Msmeterdips { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PumpCash> PumpCashes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VoucherBill> VoucherBills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BiscuitCash>(entity =>
            {
                entity.ToTable("BiscuitCash");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Calibratation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Calibratation");

                entity.Property(e => e.Diff).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 1)");
            });

            modelBuilder.Entity<Hsdmeterdip>(entity =>
            {
                entity.ToTable("Hsdmeterdip");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Hsddip1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsddip2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdmeter1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdmeter2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdmeter3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdmeter4).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdtotal1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Hsdtotal2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Shift).HasColumnName("shift");
            });

            modelBuilder.Entity<Msmeterdip>(entity =>
            {
                entity.ToTable("Msmeterdip");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.MsDip1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsDip2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsMeter1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsMeter2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsMeter3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsMeter4).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsTotal1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MsTotal2).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.PackUnit).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(255);

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PumpCash>(entity =>
            {
                entity.ToTable("PumpCash");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).HasMaxLength(255);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateofSale).HasColumnType("datetime");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyPurchased).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneNo).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<VoucherBill>(entity =>
            {
                entity.Property(e => e.Ammount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
