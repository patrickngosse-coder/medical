using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;
using medical.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace medical.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<Personnel>
    {
        public ApplicationDbContext()
            : base("hmsData", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<ActeInfiemier> ActeInfiemiers { get; set; }
        public virtual DbSet<ActeMedical> ActeMedicals { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Depense> Depenses { get; set; }
        public virtual DbSet<Demandeur> Demandeurs { get; set; }
        public virtual DbSet<CentreSante> CentreSantes { get; set; }
        public virtual DbSet<Compterendu> Compterendus { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }
        public virtual DbSet<DivisionProvincialSante> DivisionProvincialSantes { get; set; }
        public virtual DbSet<Examen> Examens { get; set; }
        public virtual DbSet<Facturation> Facturations { get; set; }
        public virtual DbSet<Hopital> Hopitals { get; set; }
        public virtual DbSet<Hospitalisation> Hospitalisations { get; set; }
        public virtual DbSet<Infirmier> Infirmiers { get; set; }
        public virtual DbSet<Maladie> Maladies { get; set; }
        public virtual DbSet<Medecin> Medecins { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Pavillon> Pavillons { get; set; }
        public virtual DbSet<Rendezvous> Rendezvouses { get; set; }
        public virtual DbSet<Serve> Services { get; set; }
        public virtual DbSet<Territoire> Territoires { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }
        public virtual DbSet<ZoneSante> ZoneSantes { get; set; }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }

    }
}