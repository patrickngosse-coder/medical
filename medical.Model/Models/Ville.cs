using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Ville
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDVILLE { get; set; }

        [Required]
        [Display(Name = "Territoire d'Appartenance")]
        public int? IDTERRITOIRE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual Territoire Territoire { get; set; }

        public virtual ICollection<Hopital> Hopitals { get; set; }
    }
}
