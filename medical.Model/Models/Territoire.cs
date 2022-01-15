using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Territoire
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDTERRITOIRE { get; set; }

        [Required]
        [Display(Name = "Province d'Appartenance")]
        public int? IDPROVINCE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Ville> Villes { get; set; }
    }
}
