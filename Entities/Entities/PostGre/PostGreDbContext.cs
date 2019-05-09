using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Entities.PostGre
{
    public partial class PostGreDbContext : DbContext
    {
        //public PostGreDbContext()
        //{
        //}

        public PostGreDbContext(DbContextOptions<PostGreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Passanger> Passanger { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseNpgsql("Server=localhost;Database=dbRisen;Username=postgres;Password=123456789;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.Property(e => e.BusId).UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.StartTimeStamp).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Drivers)
                    .WithMany(p => p.Bus)
                    .HasForeignKey(d => d.DriversId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drivers_Bus");
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.Property(e => e.DriversId).UseNpgsqlIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Passanger>(entity =>
            {
                entity.Property(e => e.PassangerId).UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.TimeStamp).HasDefaultValueSql("now()");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Passanger)
                    .HasForeignKey(d => d.BusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bus_Passanger");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK_Token");

                entity.Property(e => e.TokenId).UseNpgsqlIdentityAlwaysColumn();

                entity.Property(e => e.TimeStamp).HasDefaultValueSql("now()");
            });
        }
    }
}
