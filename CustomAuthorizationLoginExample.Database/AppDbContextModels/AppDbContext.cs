using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorizationLoginExample.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblLogin> TblLogins { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId);

            entity.ToTable("Tbl_Login");

            entity.Property(e => e.LoginId).ValueGeneratedOnAdd();
            entity.Property(e => e.SessionExpired).HasColumnType("datetime");
            entity.Property(e => e.SessionId)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_User");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
