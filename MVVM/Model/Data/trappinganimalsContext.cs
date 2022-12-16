using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static PIS8_2.MVVM.Model.Card;
using static PIS8_2.MVVM.Model.Log;
using static PIS8_2.MVVM.Model.Tuser;

namespace PIS8_2.MVVM.Model.Data;

public partial class TrappinganimalsContext : DbContext
{
    public TrappinganimalsContext()
    {
    }

    static TrappinganimalsContext()
    => NpgsqlConnection.GlobalTypeMapper
        .MapEnum<role_type>()
        .MapEnum<operation>()
        .MapEnum<order_type>();

    public TrappinganimalsContext(DbContextOptions<TrappinganimalsContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Municip> Municips { get; set; }

    public virtual DbSet<Omsu> Omsus { get; set; }

    public virtual DbSet<Organisation> Organisations { get; set; }

    public virtual DbSet<Tuser> Tusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=trappinganimals;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<operation>()
            .HasPostgresEnum<order_type>()
            .HasPostgresEnum<role_type>();


        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("card_pkey");

            entity.ToTable("card");

            entity.Property(e => e.Id).HasColumnName("id");
            
            entity.Property(e => e.Adresstrapping)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("adresstrapping");
            entity.Property(e => e.Datemk).HasColumnName("datemk");
            entity.Property(e => e.Datetrapping).HasColumnName("datetrapping");
            entity.Property(e => e.Dateworkorder).HasColumnName("dateworkorder");
            entity.Property(e => e.IdMunicip).HasColumnName("id_municip");
            entity.Property(e => e.IdOmsu).HasColumnName("id_omsu");
            entity.Property(e => e.IdOrg).HasColumnName("id_org");
            entity.Property(e => e.Locality)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("locality");
            entity.Property(e => e.Nummk).HasColumnName("nummk");
            entity.Property(e => e.Numworkorder).HasColumnName("numworkorder");
            entity.Property(e => e.Targetorder)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("targetorder");
            // ~~~ enum
            entity.Property(e => e.TypeOrder)
                .IsRequired()
                //.HasConversion(c => c.ToString(), c => Enum.Parse<TypeOrder>(c)) || Для релиза необходимо, чтобы все значения в enum DB были на англ.
                .HasColumnName("typeorder");
            entity.Property(e => e.AccessRoles)
                //.HasConversion(
                //e => e.Cast<int>().ToArray(),
                //e => e.Cast<role_type>().ToArray())
                .IsRequired()
                .HasColumnName("accessroles");

            entity.HasOne(d => d.IdMunicipNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdMunicip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_id_municip_fkey");

            entity.HasOne(d => d.IdOmsuNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdOmsu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_id_omsu_fkey");

            entity.HasOne(d => d.IdOrgNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdOrg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("card_id_org_fkey");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("log_pkey");

            entity.ToTable("log");

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date");
            entity.Property(e => e.IdCard)
                .IsRequired()
                .HasColumnName("id_card");
            entity.Property(e => e.IdUser)
                .IsRequired()
                .HasColumnName("id_user");
            // enum
            entity.Property(e => e.Operation)
                .IsRequired()
                .HasColumnName("operation");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("log_id_card_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("log_id_user_fkey");
        });

        modelBuilder.Entity<Municip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("municip_pkey");

            entity.ToTable("municip");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Namemunicip)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("namemunicip");
        });

        modelBuilder.Entity<Omsu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("omsu_pkey");

            entity.ToTable("omsu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdMunicip).HasColumnName("id_municip");
            entity.Property(e => e.Nameomsu)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nameomsu");
            entity.Property(e => e.Adress)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("adress");
            entity.Property(e => e.Firstnamedir)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("firstnamedir");
            entity.Property(e => e.Patronymicdir)
                .HasMaxLength(50)
                .HasColumnName("patronymicdir");
            entity.Property(e => e.Phonenumber)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Surnamedir)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("surnamedir");

            entity.HasOne(d => d.IdMunicipNavigation).WithMany(p => p.Omsus)
                .HasForeignKey(d => d.IdMunicip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("omsu_id_municip_fkey");
        });

        modelBuilder.Entity<Organisation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organisation_pkey");

            entity.ToTable("organisation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("adress");
            entity.Property(e => e.Firstnamedir)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("firstnamedir");
            entity.Property(e => e.Nameorg)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nameorg");
            entity.Property(e => e.Patronymicdir)
                .HasMaxLength(50)
                .HasColumnName("patronymicdir");
            entity.Property(e => e.Phonenumber)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Surnamedir)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("surnamedir");
        });

        modelBuilder.Entity<Tuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tuser_pkey");

            entity.ToTable("tuser");

            entity.HasIndex(e => e.Login, "tuser_login_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdOmsu).HasColumnName("id_omsu");
            entity.Property(e => e.IdOrg).HasColumnName("id_org");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Passwordhash)
                .IsRequired()
                .HasMaxLength(66)
                .HasColumnName("passwordhash");
            // enum
            entity.Property(e => e.Role)
                .IsRequired()
                .HasColumnName("role");

            entity.HasOne(d => d.IdOmsuNavigation).WithMany(p => p.Tusers)
                .HasForeignKey(d => d.IdOmsu)
                .HasConstraintName("tuser_id_omsu_fkey");

            entity.HasOne(d => d.IdOrgNavigation).WithMany(p => p.Tusers)
                .HasForeignKey(d => d.IdOrg)
                .HasConstraintName("tuser_id_org_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
