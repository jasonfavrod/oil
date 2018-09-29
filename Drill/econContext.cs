using System;
using Nini.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Drill
{
    public partial class econContext : DbContext
    {
        public econContext()
        {
        }

        public econContext(DbContextOptions<econContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Oil> Oil { get; set; }

        // Unable to generate entity type for table 'price_data.gold'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = "";
                IConfigSource configSource = new IniConfigSource("config.ini");
                IConfig dbConfig = configSource.Configs["Database"];

                connString += "Host="       + dbConfig.Get("host") + ";";
                connString += "Database="   + dbConfig.Get("database") + ";";
                connString += "Username="   + dbConfig.Get("username") + ";";
                connString += "Password="   + dbConfig.Get("password");

                optionsBuilder.UseNpgsql(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oil>(entity =>
            {
                entity.ToTable("oil", "price_data");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('price_data.oil_id_seq'::regclass)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(7,2)");

                entity.Property(e => e.SampleDate)
                    .HasColumnName("sample_date")
                    .HasColumnType("date");

                entity.Property(e => e.SampleTime)
                    .HasColumnName("sample_time")
                    .HasColumnType("time without time zone")
                    .ForNpgsqlHasComment("Should be UTC");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasMaxLength(255);

                entity.Property(e => e.Uom)
                    .HasColumnName("uom")
                    .HasMaxLength(64);
            });

            modelBuilder.HasSequence("oil_id_seq");
        }
    }
}
