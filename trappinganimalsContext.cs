using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PIS8_2.MVVM.Model;

namespace PIS8_2
{
    public partial class trappinganimalsContext : DbContext
    {
        public trappinganimalsContext()
        {
        }

        public trappinganimalsContext(DbContextOptions<trappinganimalsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Tuser> Tusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TrappingAnimals;Username=postgres;Password=2476");
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=trappinganimals;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("operation", new[] { "Удаление карточки из реестра", "Добавление карточки в реестр", "Изменение карточки", "Удаление файла" })
                .HasPostgresEnum("order_type", new[] { "План-график", "Заказ-наряд" })
                .HasPostgresEnum("role_type", new[] { "Оператор по отлову", "Куратор ВетСлужбы", "Куратор ОМСУ", "Куратор по отлову", "Оператор ВетСлужбы", "Оператор ОМСУ", "Подписант ВетСлужбы", "Подписант ОМСУ", "Подписант по отлову" });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("animal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Animaltype)
                    .HasMaxLength(50)
                    .HasColumnName("animaltype");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Ears)
                    .HasMaxLength(50)
                    .HasColumnName("ears");

                entity.Property(e => e.Hair)
                    .HasMaxLength(50)
                    .HasColumnName("hair");

                entity.Property(e => e.Kingcolor)
                    .HasMaxLength(50)
                    .HasColumnName("kingcolor");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Tail)
                    .HasMaxLength(50)
                    .HasColumnName("tail");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("card");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Animalid).HasColumnName("animalid");

                entity.Property(e => e.Datemk).HasColumnName("datemk");

                entity.Property(e => e.Dateworkorder).HasColumnName("dateworkorder");

                entity.Property(e => e.Executormk)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("executormk");

                entity.Property(e => e.IdOrg).HasColumnName("id_org");

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("locality");

                entity.Property(e => e.Nummk).HasColumnName("nummk");

                entity.Property(e => e.Numworkorder).HasColumnName("numworkorder");

                entity.Property(e => e.Omsu)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("omsu");

                entity.Property(e => e.Targetorder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("targetorder");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Animalid)
                    .HasConstraintName("card_animalid_fkey");

                entity.HasOne(d => d.IdOrgNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.IdOrg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("card_id_org_fkey");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cardid).HasColumnName("cardid");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.Cardid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_cardid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_userid_fkey");
            });

            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.ToTable("organisation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.ToTable("tuser");

                entity.HasIndex(e => e.Login, "tuser_login_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrg).HasColumnName("id_org");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Passwordhash)
                    .IsRequired()
                    .HasMaxLength(66)
                    .HasColumnName("passwordhash");

                entity.HasOne(d => d.IdOrgNavigation)
                    .WithMany(p => p.Tusers)
                    .HasForeignKey(d => d.IdOrg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tuser_id_org_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
