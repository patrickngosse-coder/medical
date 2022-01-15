using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    
    public class Compterendu
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDCMPTERENDU { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Rapport")]
        public string RAPPORT { get; set; }

        [Required]
        [Display(Name = "Acte Médical")]
        public int? IDACTMEDICO { get; set; }

        [Required]
        [Display(Name = "Acte Infirmier")]
        public int? IDACTEINFIRMIER { get; set; }

        [Required]
        [Display(Name = "Examen")]
        public int? EXAMENID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Pathologie")]
        public string PATHOLOGIE { get; set; }

        public virtual Examen Examen { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ActeInfiemier ActeInfiemier { get; set; }
        public virtual ActeMedical ActeMedical { get; set; }
    }
}
