namespace medical.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class MedicalConfiguration : DbMigrationsConfiguration<medical.Data.ApplicationDbContext>
    {
        public MedicalConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(medical.Data.ApplicationDbContext context)
        {
            
            new List<IdentityRole>
            {
                new IdentityRole{ Name ="Admin" },
                new IdentityRole{ Name ="Medecin" },
                new IdentityRole{ Name ="Caissier" },
                new IdentityRole{ Name ="Secretaire"}

            }.ForEach(m => context.Roles.Add(m));

            context.Commit();
        }
    }
}
