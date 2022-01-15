using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public enum PatientEtatCivil
    {
        Célibataire,
        Marié,
        Veuf,
        Divorcé
    }

    public enum PatientSexe
    {
        Homme,
        Femme
    }

    public class Patient
    {
        [Key]
        [ScaffoldColumn(false)]
        public System.Guid IDPATIENT { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nom")]
        public string NOM { get; set; }

     
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "PostNom")]
        public string POSTNOM { get; set; }

        
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Prénom")]
        public string PRENOM { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Sexe")]
        public string SEXE { get; set; }

       
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Lieu de Naissance")]
        public string LIEUNAISSANCE { get; set; }

    
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Naissance")]
        public DateTime DATENAISSANCE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Etat-Civil")]
        public PatientEtatCivil? ETATCIVIL { get; set; }

      
        [Display(Name = "Téléphone")]
        public string TEL { get; set; }

      
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
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

        public DateTime DateEnregistrement { get; set; }
        public virtual ICollection<ActeMedical> ActeMedicals { get; set; }
        public virtual ICollection<ActeInfiemier> ActeInfiemiers { get; set; }
        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<Hospitalisation> Hospitalisations { get; set; }
        public virtual ICollection<Rendezvous> Rendezvouss { get; set; }
        public virtual ICollection<Facturation> Facturations { get; set; }
        public virtual ICollection<Maladie> Maladies { get; set; }
    }
}

