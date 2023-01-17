using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RoleUser.Models
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserName> UserNames { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseLazyLoadingProxies();
               
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=D-OS3-TUANVD2\\MSSQLSERVER01;Database=Employee;User Id=sa;Password=tuantam121121;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsFixedLength(true);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<UserName>(entity =>
            {
                entity.ToTable("UserName");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserNames)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Group");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
