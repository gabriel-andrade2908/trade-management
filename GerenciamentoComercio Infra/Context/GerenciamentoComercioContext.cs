using GerenciamentoComercio_Infra.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GerenciamentoComercio_Infra.Context
{
    public partial class GerenciamentoComercioContext : DbContext
    {
        public GerenciamentoComercioContext()
        {
        }

        public GerenciamentoComercioContext(DbContextOptions<GerenciamentoComercioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientTransaction> ClientTransaction { get; set; }
        public virtual DbSet<ClientTransactionProduct> ClientTransactionProduct { get; set; }
        public virtual DbSet<ClientTransactionService> ClientTransactionService { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAccess> EmployeeAccess { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductHistoric> ProductHistoric { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategory { get; set; }
        public virtual DbSet<ServiceHistoric> ServiceHistoric { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=74.50.111.162;Database=GerenciamentoComercio;User ID=latecher;Password=Lealdrade123@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("latecher")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Access>(entity =>
            {
                entity.ToTable("ACCESS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Type)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("CLIENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<ClientTransaction>(entity =>
            {
                entity.ToTable("CLIENT_TRANSACTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.DiscountPercentage).HasColumnName("DISCOUNT_PERCENTAGE");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("money")
                    .HasColumnName("DISCOUNT_PRICE");

                entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.Property(e => e.Observations)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVATIONS");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("TOTAL_PRICE");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.ClientTransaction)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK__CLIENT_TR__ID_CL__36B12243");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ClientTransaction)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK__CLIENT_TR__ID_EM__37A5467C");
            });

            modelBuilder.Entity<ClientTransactionProduct>(entity =>
            {
                entity.ToTable("CLIENT_TRANSACTION_PRODUCT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.IdClientTransaction).HasColumnName("ID_CLIENT_TRANSACTION");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdClientTransactionNavigation)
                    .WithMany(p => p.ClientTransactionProduct)
                    .HasForeignKey(d => d.IdClientTransaction)
                    .HasConstraintName("FK__CLIENT_TR__ID_CL__440B1D61");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ClientTransactionProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__CLIENT_TR__ID_PR__44FF419A");
            });

            modelBuilder.Entity<ClientTransactionService>(entity =>
            {
                entity.ToTable("CLIENT_TRANSACTION_SERVICE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.IdClientTransaction).HasColumnName("ID_CLIENT_TRANSACTION");

                entity.Property(e => e.IdService).HasColumnName("ID_SERVICE");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdClientTransactionNavigation)
                    .WithMany(p => p.ClientTransactionService)
                    .HasForeignKey(d => d.IdClientTransaction)
                    .HasConstraintName("FK__CLIENT_TR__ID_CL__403A8C7D");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.ClientTransactionService)
                    .HasForeignKey(d => d.IdService)
                    .HasConstraintName("FK__CLIENT_TR__ID_SE__412EB0B6");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Access)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ACCESS");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.IsAdministrator).HasColumnName("IS_ADMINISTRATOR");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phone)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<EmployeeAccess>(entity =>
            {
                entity.ToTable("EMPLOYEE_ACCESS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.IdAccess).HasColumnName("ID_ACCESS");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.HasOne(d => d.IdAccessNavigation)
                    .WithMany(p => p.EmployeeAccess)
                    .HasForeignKey(d => d.IdAccess)
                    .HasConstraintName("FK__EMPLOYEE___ID_AC__3D5E1FD2");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.EmployeeAccess)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK__EMPLOYEE___ID_EM__3C69FB99");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdProductCategory).HasColumnName("ID_PRODUCT_CATEGORY");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.IdProductCategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdProductCategory)
                    .HasConstraintName("FK__PRODUCT__ID_PROD__2E1BDC42");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("PRODUCT_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<ProductHistoric>(entity =>
            {
                entity.ToTable("PRODUCT_HISTORIC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductHistoric)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__PRODUCT_H__ID_PR__33D4B598");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IdServiceCategory).HasColumnName("ID_SERVICE_CATEGORY");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Sla)
                    .HasColumnType("money")
                    .HasColumnName("SLA");

                entity.HasOne(d => d.IdServiceCategoryNavigation)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.IdServiceCategory)
                    .HasConstraintName("FK__SERVICE__ID_SERV__2B3F6F97");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.ToTable("SERVICE_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<ServiceHistoric>(entity =>
            {
                entity.ToTable("SERVICE_HISTORIC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_DATE");

                entity.Property(e => e.CreationUser)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CREATION_USER");

                entity.Property(e => e.IdService).HasColumnName("ID_SERVICE");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Sla)
                    .HasColumnType("money")
                    .HasColumnName("SLA");

                entity.HasOne(d => d.IdServiceNavigation)
                    .WithMany(p => p.ServiceHistoric)
                    .HasForeignKey(d => d.IdService)
                    .HasConstraintName("FK__SERVICE_H__ID_SE__30F848ED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
