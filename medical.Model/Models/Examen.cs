using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public class Examen
    {
      
        [Key]
        [ScaffoldColumn(false)]
        public int IDEXAMEN { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Code Examen")]
        public string CODE { get; set; }

       
        [Display(Name = "Service d'Appartenance")]
        public int? IDSERVICE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "n/a", ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Display(Name = "Prix Unitaire")]
        public double PRIXUNITAIRE { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Matière")]
        public double MATIERE { get; set; }

        public string Souche
        {
            get
            {
                return DESIGNATION + " : " + PRIXUNITAIRE.ToString();
            }
        }

        [DisplayFormat(NullDisplayText = "n/a", ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Display(Name = "Prix Unitaire")]
        [DataType(DataType.Currency)]
        public double Cout
        {
            get
            {
                return PRIXUNITAIRE + MATIERE;
            }
        }

        public virtual Serve Service { get; set; }

        public virtual ICollection<Facturation> Facturations { get; set; }

    }
}
