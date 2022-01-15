using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Demandeur
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDDEMANDEUR { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nom")]
        public string NOM { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "PostNom")]
        public string POSTNOM { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Prénom")]
        public string PRENOM { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Sexe")]
        public PersonnelSexe? SEXE { get; set; }

        public string NomComplet
        {
            get
            {
                return NOM + " " + POSTNOM + " " + PRENOM;
            }
        }

        public virtual ICollection<Facturation> Facturations { get; set; }
    }
}
