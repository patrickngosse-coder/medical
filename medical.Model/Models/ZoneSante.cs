using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class ZoneSante
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDZONE { get; set; }

        [Required]
        [Display(Name = "DPS d'Appartenance")]
        public int? IDDIVISION { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual DivisionProvincialSante DivisionProvincialSante { get; set; }

        public virtual ICollection<CentreSante> CentreSantes { get; set; }
    }
}
