using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Materiel
    {
        public Materiel()
        {
            DateEdition = DateTime.Now;
        }
          

        [Key]
        [ScaffoldColumn(false)]
        public int IDMATERIEL { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Code ")]
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
        [Display(Name = "Prix d'Achat")]
        public double PRIXUNITAIRE { get; set; }

      
        public virtual Serve Service { get; set; }
        public DateTime DateEdition { get; set; }
    }
}
