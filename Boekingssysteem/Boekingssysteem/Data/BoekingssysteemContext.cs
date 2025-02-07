using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using Boekingssysteem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Boekingssysteem.Areas.Identity.Data;
using Boekingssysteem.ViewModels;

namespace Boekingssysteem.Data
{
    public class BoekingssysteemContext : IdentityDbContext<CustomUser>
    {
        public BoekingssysteemContext(DbContextOptions<BoekingssysteemContext> options) : base(options) { }

        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Afwezigheid> Afwezigheden { get; set; }
        public DbSet<PersoonRichting> PersoonRichtingen { get; set; }
        public DbSet<Richting> Richtingen { get; set; }
        public DbSet<PersoonFunctie> PersoonFuncties { get; set; }
        public DbSet<Functie> Functies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("TM_Boekingssysteem");

            //Tabellen aanmaken
            modelBuilder.Entity<Persoon>().ToTable("Persoon");
            modelBuilder.Entity<Afwezigheid>().ToTable("Afwezigheid");
            modelBuilder.Entity<PersoonRichting>().ToTable("PersoonRichting");
            modelBuilder.Entity<Richting>().ToTable("Richting");
            modelBuilder.Entity<PersoonFunctie>().ToTable("PersoonFunctie");
            modelBuilder.Entity<Functie>().ToTable("Functie");

            //Navigatietabellen
            /*Persoon kan meerdere afwezigheden hebben*/
            modelBuilder.Entity<Afwezigheid>().HasOne(p => p.Persoon).WithMany(a => a.Afwezigheden).HasForeignKey(p => p.Personeelnummer).IsRequired();

            /*PersoonRichting heeft één persoon*/
            modelBuilder.Entity<PersoonRichting>().HasKey(pr => new { pr.RichtingID, pr.Personeelnummer });

            /*Persoon en Richting naar PersoonRichting*/
            modelBuilder.Entity<PersoonRichting>().HasOne(pr => pr.Persoon).WithMany(x => x.PersoonRichtingen).HasForeignKey(pr => pr.Personeelnummer).IsRequired();
            modelBuilder.Entity<PersoonRichting>().HasOne(pr => pr.Richting).WithMany(x => x.PersoonRichtingen).HasForeignKey(pr => pr.RichtingID).IsRequired();

            /*PersoonFunctie heeft één Persoon*/
            modelBuilder.Entity<PersoonFunctie>().HasKey(pf => new { pf.FunctieID, pf.Personeelnummer });

            /*Persoon en Functie naar PersoonFunctie*/
            modelBuilder.Entity<PersoonFunctie>().HasOne(pf => pf.Persoon).WithMany(x => x.PersoonFuncties).HasForeignKey(pr => pr.Personeelnummer).IsRequired();
            modelBuilder.Entity<PersoonFunctie>().HasOne(pf => pf.Functie).WithMany(x => x.PersoonFuncties).HasForeignKey(pr => pr.FunctieID).IsRequired();
        }
    }
}
