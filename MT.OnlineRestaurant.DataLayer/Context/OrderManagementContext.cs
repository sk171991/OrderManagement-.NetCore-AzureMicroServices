using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    [ExcludeFromCodeCoverage]
    public partial class OrderManagementContext : DbContext
    {
        private readonly string DbConnectionString;
        public OrderManagementContext()
        {
        }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        {
        }

        public OrderManagementContext(string connectionstring) 
        {
            DbConnectionString = connectionstring;
        }

        public virtual DbSet<LoggingInfo> LoggingInfo { get; set; }
        public virtual DbSet<TblCartItems> TblCartItems { get; set; }
        public virtual DbSet<TblItemOutOfStock> TblItemOutOfStocks { get; set; }
        public virtual DbSet<TblFoodOrder> TblFoodOrder { get; set; }
        public virtual DbSet<TblFoodOrderMapping> TblFoodOrderMapping { get; set; }
        public virtual DbSet<TblOrderPayment> TblOrderPayment { get; set; }
        public virtual DbSet<TblOrderStatus> TblOrderStatus { get; set; }
        public virtual DbSet<TblPaymentStatus> TblPaymentStatus { get; set; }
        public virtual DbSet<TblPaymentType> TblPaymentType { get; set; }
        public virtual DbSet<TblTableOrder> TblTableOrder { get; set; }
        public virtual DbSet<TblTableOrderMapping> TblTableOrderMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=tcp:capstoneteam1server.database.windows.net,1433;Initial Catalog=OrderManagement;Persist Security Info=False;user id=cpadmin;password=Mindtree@12;");
                optionsBuilder.UseSqlServer(DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<LoggingInfo>(entity =>
            {
                entity.Property(e => e.ActionName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");
            });
            modelBuilder.Entity<TblCartItems>(entity =>
            {
                entity.ToTable("tblCartItems");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblMenuId)
                    .HasColumnName("tblMenuID")
                    .HasDefaultValueSql("((0))");


                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsItemAvailable).HasColumnType("bit");

            });

            modelBuilder.Entity<TblItemOutOfStock>(entity =>
            {
                entity.ToTable("tblItemOutOfStock");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("TblRestaurantId")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblMenuId)
                    .HasColumnName("tblMenuID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsItemAvailable).HasColumnType("bit");

            });



            modelBuilder.Entity<TblFoodOrder>(entity =>
            {
                entity.ToTable("tblFoodOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblOrderStatusId)
                    .HasColumnName("tblOrderStatusID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblPaymentTypeId)
                    .HasColumnName("tblPaymentTypeID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.TblOrderStatus)
                    .WithMany(p => p.TblFoodOrder)
                    .HasForeignKey(d => d.TblOrderStatusId)
                    .HasConstraintName("FK_tblFoodOrder_tblOrderStatusID");

                entity.HasOne(d => d.TblPaymentType)
                    .WithMany(p => p.TblFoodOrder)
                    .HasForeignKey(d => d.TblPaymentTypeId)
                    .HasConstraintName("FK_tblFoodOrder_tblPaymentTypeID");
            });

            modelBuilder.Entity<TblFoodOrderMapping>(entity =>
            {
                entity.ToTable("tblFoodOrderMapping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblFoodOrderId)
                    .HasColumnName("tblFoodOrderID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblMenuId)
                    .HasColumnName("tblMenuID")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblFoodOrder)
                    .WithMany(p => p.TblFoodOrderMapping)
                    .HasForeignKey(d => d.TblFoodOrderId)
                    .HasConstraintName("FK_tblFoodOrderMapping_tblFoodOrderID");
            });

            modelBuilder.Entity<TblOrderPayment>(entity =>
            {
                entity.ToTable("tblOrderPayment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TblCustomerId).HasColumnName("tblCustomerID");

                entity.Property(e => e.TblFoodOrderId).HasColumnName("tblFoodOrderID");

                entity.Property(e => e.TblPaymentStatusId).HasColumnName("tblPaymentStatusID");

                entity.Property(e => e.TblPaymentTypeId).HasColumnName("tblPaymentTypeID");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasColumnName("TransactionID")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('0000000000')");

                entity.HasOne(d => d.TblFoodOrder)
                    .WithMany(p => p.TblOrderPayment)
                    .HasForeignKey(d => d.TblFoodOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrderPayment_tblFoodOrderID");

                entity.HasOne(d => d.TblPaymentStatus)
                    .WithMany(p => p.TblOrderPayment)
                    .HasForeignKey(d => d.TblPaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrderPayment_tblPaymentStatusID");

                entity.HasOne(d => d.TblPaymentType)
                    .WithMany(p => p.TblOrderPayment)
                    .HasForeignKey(d => d.TblPaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOrderPayment_tblPaymentType");
            });

            modelBuilder.Entity<TblOrderStatus>(entity =>
            {
                entity.ToTable("tblOrderStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblPaymentStatus>(entity =>
            {
                entity.ToTable("tblPaymentStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblPaymentType>(entity =>
            {
                entity.ToTable("tblPaymentType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTableOrder>(entity =>
            {
                entity.ToTable("tblTableOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblOrderStatusId)
                    .HasColumnName("tblOrderStatusID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblPaymentTypeId)
                    .HasColumnName("tblPaymentTypeID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblOrderStatus)
                    .WithMany(p => p.TblTableOrder)
                    .HasForeignKey(d => d.TblOrderStatusId)
                    .HasConstraintName("FK_tblTableOrder_tblOrderStatusID");

                entity.HasOne(d => d.TblPaymentType)
                    .WithMany(p => p.TblTableOrder)
                    .HasForeignKey(d => d.TblPaymentTypeId)
                    .HasConstraintName("FK_tblTableOrder_tblPaymentTypeID");
            });

            modelBuilder.Entity<TblTableOrderMapping>(entity =>
            {
                entity.ToTable("tblTableOrderMapping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TableNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.TblTableOrderId)
                    .HasColumnName("tblTableOrderID")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblTableOrder)
                    .WithMany(p => p.TblTableOrderMapping)
                    .HasForeignKey(d => d.TblTableOrderId)
                    .HasConstraintName("FK_tblTableOrderMapping_tblTableOrderID");
            });
        }
    }
}
