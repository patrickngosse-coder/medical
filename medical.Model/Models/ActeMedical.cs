using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
   
    public class ActeMedical
    {
       
        [Key]
        [ScaffoldColumn(false)]
        public int IDACTEMEDICAUX { get; set; }

        [Required]
        [Display(Name = "Patient")]
        public int? IDPATIENT { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Acte")]
        public string ACTE { get; set; }

        [Display(Name = "Médecin Prestataire")]
        public string Id { get; set; }

        public virtual Medecin Medecin { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<Compterendu> Compterendus { get; set; }
    }
}
