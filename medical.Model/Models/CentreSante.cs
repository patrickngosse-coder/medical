using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace medical.Model.Models
{
    public class CentreSante
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDCENTRE { get; set; }

        [Required]
        [Display(Name = "Zone de Sante d'Appartenance")]
        public int? IDZONE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual ZoneSante ZoneSante { get; set; }
        public virtual ICollection<Infirmier> Infirmiers { get; set; }
        public virtual ICollection<Maladie> Maladies { get; set; }
    }
}