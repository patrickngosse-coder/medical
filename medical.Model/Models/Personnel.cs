using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    public enum PersonnelSexe
    {
        Homme,
        Femme
    }

    public enum PersonnelEtatCivil
    {
        Marié,
        Célibataire,
        Veuf,
        Divorcé
    }
    public class Personnel : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Personnel> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
      
        [DataType(DataType.Text)]
        [Display(Name = "Nom")]
        public string NOM { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "PostNom")]
        public string POSTNOM { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Prénom")]
        public string PRENOM { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Sexe")]
        public PersonnelSexe? SEXE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Lieu de Naissance")]
        public string LIEUNAISSANCE { get; set; }

        [Display(Name = "Date De naissance")]
        public DateTime? DATENAISSANCE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Etat-Civil")]
        public PersonnelEtatCivil? ETATCIVIL { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Matricule")]
        public string MATRICULE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Grade")]
        public string GRADE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Service d'Affectation")]
        public int? IDSERVICE { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Adresse")]
        public string ADRESSSE { get; set; }

        public string NomComplet
        {
            get
            {
                return NOM + " " + POSTNOM + " " + PRENOM;
            }
        }

        public virtual Serve Service { get; set; }
    }
}