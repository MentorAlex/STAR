﻿using STAR.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STAR.Data {
    public class StarContext : DbContext {
        public StarContext() : base("StarContext") {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StarContext, Migrations.Configuration>("StarContext"));
        }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<Contractor> Contractors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Contractor>().Property(c => c.FirstName).IsRequired();
            modelBuilder.Entity<Contractor>().Property(c => c.LastName).IsRequired();

            modelBuilder.Entity<Skill>().Property(s => s.Name)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            modelBuilder.Entity<Skill>().Property(s => s.Description).HasMaxLength(256);
        }
    }
}
