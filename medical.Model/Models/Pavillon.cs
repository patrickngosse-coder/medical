using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
   
    public class Pavillon
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDPAVILLON { get; set; }

        [Required]
        [Display(Name = "Service d'Appartenance")]
        public int? IDSERVICE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        public virtual Serve Service { get; set; }
        public virtual ICollection<Hospitalisation> Hospitalisations { get; set; }

    }
}
