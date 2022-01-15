using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
  
    public class ActeInfiemier
    {
        
        [Key]
        [ScaffoldColumn(false)]
        public int IDACTEINFIRMIER { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public int? IDPATIENT { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Acte")]
        public string ACTE { get; set; }

        [Display(Name = "Infirmier Prestataire")]
        public string Id { get; set; }

        public virtual Infirmier Infirmier { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<Compterendu> Compterendus { get; set; }
    }
}
