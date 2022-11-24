using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PIS8_2.MVVM.Model
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
        public virtual DbSet<Omsu> Omsus { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Tuser> Tusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=trappinganimals;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("animal_hair", new[] { "Короткошёрстная", "Длинношёрстная", "Жесткошёрстная", "Кудрявая" })
                .HasPostgresEnum("animal_size", new[] { "Большой", "Средний", "Маленький" })
                .HasPostgresEnum("animal_type", new[] { "Кошка", "Котёнок", "Собака", "Щенок" })
                .HasPostgresEnum("applicant_type", new[] { "Физическое лицо", "Юридическое лицо" })
                .HasPostgresEnum("operation", new[] { "Удаление карточки из реестра", "Добавление карточки в реестр", "Изменение карточки", "Удаление файла" })
                .HasPostgresEnum("order_type", new[] { "План-график", "Заказ-наряд" })
                .HasPostgresEnum("role_type", new[] { "Оператор по отлову", "Куратор ВетСлужбы", "Куратор ОМСУ", "Куратор по отлову", "Оператор ВетСлужбы", "Оператор ОМСУ", "Подписант ВетСлужбы", "Подписант ОМСУ", "Подписант по отлову" });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("animal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Ears)
                    .HasMaxLength(50)
                    .HasColumnName("ears");

                entity.Property(e => e.Kingcolor)
                    .HasMaxLength(50)
                    .HasColumnName("kingcolor");

                entity.Property(e => e.Tail)
                    .HasMaxLength(50)
                    .HasColumnName("tail");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("card");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adressappl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("adressappl");

                entity.Property(e => e.Adresstrapping)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("adresstrapping");

                entity.Property(e => e.Animalid).HasColumnName("animalid");

                entity.Property(e => e.Datemk).HasColumnName("datemk");

                entity.Property(e => e.Datetrapping).HasColumnName("datetrapping");

                entity.Property(e => e.Dateworkorder).HasColumnName("dateworkorder");

                entity.Property(e => e.Firstnameappl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstnameappl");

                entity.Property(e => e.Firstnameexecuter)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstnameexecuter");

                entity.Property(e => e.IdOrg).HasColumnName("id_org");

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("locality");

                entity.Property(e => e.Nummk).HasColumnName("nummk");

                entity.Property(e => e.Numworkorder).HasColumnName("numworkorder");

                entity.Property(e => e.Omsu).HasColumnName("omsu");

                entity.Property(e => e.Patronymicappl)
                    .HasMaxLength(50)
                    .HasColumnName("patronymicappl");

                entity.Property(e => e.Patronymicexecuter)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("patronymicexecuter");

                entity.Property(e => e.Phonenumberappl)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("phonenumberappl");

                entity.Property(e => e.Phonenumberexecuter)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("phonenumberexecuter");

                entity.Property(e => e.Surnameappl)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("surnameappl");

                entity.Property(e => e.Surnameexecuter)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("surnameexecuter");

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

                entity.HasOne(d => d.OmsuNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Omsu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("card_omsu_fkey");
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

            modelBuilder.Entity<Omsu>(entity =>
            {
                entity.ToTable("omsu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Munformation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("munformation");

                entity.Property(e => e.Nameomsu)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nameomsu");
            });

            modelBuilder.Entity<Organisation>(entity =>
            {
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
                entity.ToTable("tuser");

                entity.HasIndex(e => e.Login, "tuser_login_key")
                    .IsUnique();

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

                entity.HasOne(d => d.IdOmsuNavigation)
                    .WithMany(p => p.Tusers)
                    .HasForeignKey(d => d.IdOmsu)
                    .HasConstraintName("tuser_id_omsu_fkey");

                entity.HasOne(d => d.IdOrgNavigation)
                    .WithMany(p => p.Tusers)
                    .HasForeignKey(d => d.IdOrg)
                    .HasConstraintName("tuser_id_org_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
