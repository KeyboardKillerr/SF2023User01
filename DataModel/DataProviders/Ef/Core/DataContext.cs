using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataProviders.Ef.Core
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-P105T05; Initial Catalog = Demo; Integrated Security = True;");
            }
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventInfo> EventInfos { get; set; }
        public virtual DbSet<EventsDirection> EventsDirections { get; set; }
        public virtual DbSet<EventsJudge> EventsJudges { get; set; }
        public virtual DbSet<Judge> Judges { get; set; }
        public virtual DbSet<Moderator> Moderators { get; set; }
        public virtual DbSet<UsersA> UsersAs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId)
                    .ValueGeneratedNever()
                    .HasColumnName("city_id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .HasColumnName("city_name");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId)
                    .ValueGeneratedNever()
                    .HasColumnName("country_id");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.Property(e => e.DirectionId)
                    .ValueGeneratedNever()
                    .HasColumnName("direction_id");

                entity.Property(e => e.DirectionName)
                    .HasMaxLength(50)
                    .HasColumnName("direction_name");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("event_id");

                entity.Property(e => e.Activity)
                    .HasMaxLength(100)
                    .HasColumnName("activity");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Days).HasColumnName("days");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .HasColumnName("event_name");

                entity.Property(e => e.ModeratorId).HasColumnName("moderator_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.Winner)
                    .HasMaxLength(100)
                    .HasColumnName("winner");

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ModeratorId)
                    .HasConstraintName("FK_Events_Moderators");
            });

            modelBuilder.Entity<EventInfo>(entity =>
            {
                entity.ToTable("Event_Info");

                entity.Property(e => e.EventInfoId)
                    .ValueGeneratedNever()
                    .HasColumnName("event_info_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Photo)
                    .HasColumnType("image")
                    .HasColumnName("photo");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventInfos)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Event_Info_Events");
            });

            modelBuilder.Entity<EventsDirection>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.DirectionId });

                entity.ToTable("Events_Directions");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.DirectionId).HasColumnName("direction_id");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.EventsDirections)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Directions_Directions");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventsDirections)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Directions_Events");
            });

            modelBuilder.Entity<EventsJudge>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.JudgeId });

                entity.ToTable("Events_Judges");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.JudgeId).HasColumnName("judge_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventsJudges)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Judges_Events");

                entity.HasOne(d => d.Judge)
                    .WithMany(p => p.EventsJudges)
                    .HasForeignKey(d => d.JudgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Judges_Judges");
            });

            modelBuilder.Entity<Judge>(entity =>
            {
                entity.Property(e => e.JudgeId)
                    .ValueGeneratedNever()
                    .HasColumnName("judge_id");

                entity.Property(e => e.JudgeName)
                    .HasMaxLength(100)
                    .HasColumnName("judge_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Moderator>(entity =>
            {
                entity.Property(e => e.ModeratorId)
                    .ValueGeneratedNever()
                    .HasColumnName("moderator_id");

                entity.Property(e => e.ModeratorName)
                    .HasMaxLength(100)
                    .HasColumnName("moderator_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UsersA>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Users");

                entity.ToTable("Users_a");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.DirectionId).HasColumnName("direction_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UsersAs)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Users_Countries");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UsersAs)
                    .HasForeignKey(d => d.DirectionId)
                    .HasConstraintName("FK_Users_Directions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
