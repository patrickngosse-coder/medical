using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public enum StatutHopital
    {
        Conventionné,
        Public,
        Privé
    }
  
    public class Hopital
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDHOPITAL { get; set; }

        [Required]
        [Display(Name = "Ville d'Appartenance")]
        public int? IDVILLE { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Coordonnee Geographique")]
        public string COORDONNE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        [Display(Name = "Statut d'Hopital")]
        public StatutHopital StatutHopital { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Acte Juridique")]
        public string ACTEJURIDIQUE { get; set; }

        public virtual Ville Ville { get; set; }

        //public virtual ICollection<Medecin> Medecins { get; set; }
        //public virtual ICollection<Infirmier> Infirmiers { get; set; } 
        //public virtual ICollection<Serve> Serves { get; set; }
        public virtual ICollection<Maladie> Maladies { get; set; }
    }
}

