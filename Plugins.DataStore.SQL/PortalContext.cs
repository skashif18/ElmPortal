using CoreBusiness.Base;
using CoreBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Infrastructure.Services;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQL;

public partial class PortalContext : DbContext
{

    private readonly ICurrentUserServiceDB currentUser;

    public PortalContext(DbContextOptions<PortalContext> options, ICurrentUserServiceDB currentUser)
        : base(options)
    {
        this.currentUser = currentUser;
    }
    // Employee Dtaset
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Status> Status { get; set; }
    public virtual DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.CreationUserName).HasMaxLength(550);

            entity.Property(e => e.Designation).HasMaxLength(550);

            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.Property(e => e.LastUpdateUserName).HasMaxLength(550);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(70);

            entity.Property(e => e.Designation)
                .HasMaxLength(250);

            entity.Property(e => e.ManagerId).HasDefaultValue(0);

            entity.Property(e => e.IsManager).HasDefaultValue(false);

        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.CreationUserName).HasMaxLength(550);

            entity.Property(e => e.NameEn)
                 .IsRequired()
                 .HasMaxLength(250);

            entity.Property(e => e.NameAr)
               .IsRequired()
               .HasMaxLength(250);

            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.Property(e => e.LastUpdateUserName).HasMaxLength(550);

        });

        modelBuilder.Entity<EmployeeLeave>(entity =>
        {
            entity.ToTable("EmployeeLeave");

            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(dateadd(hour,(3),getutcdate()))");

            entity.Property(e => e.CreationUserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.DeletedBy).HasMaxLength(250);

            entity.Property(e => e.DeletedOn).HasColumnType("datetime");

            entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

            entity.Property(e => e.LastUpdateUserName).HasMaxLength(50);

            entity.Property(e => e.Reason).HasMaxLength(550);
            entity.Property(e => e.StatusId);
            entity.Property(e => e.Comment).HasMaxLength(750);

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeLeave)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeLeave_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyAuditInformation();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {

        ApplyAuditInformation();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ApplyAuditInformation()
        => this.ChangeTracker
            .Entries()
            .ToList()
            .ForEach(entry =>
            {
                var userName = this.currentUser.GetUserName();
                if (entry.Entity is IDeletableEntity deletableEntity)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        deletableEntity.DeletedOn = DateTime.UtcNow;
                        deletableEntity.DeletedBy = userName;
                        deletableEntity.IsDeleted = true;

                        entry.State = EntityState.Modified;
                    }
                }
                if (entry.Entity is IEntity entity)
                {

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreationDate = DateTime.UtcNow;
                        entity.CreationUserName = userName;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.LastUpdateDate = DateTime.UtcNow;
                        entity.LastUpdateUserName = userName;
                    }
                }
            })
        ;
}
