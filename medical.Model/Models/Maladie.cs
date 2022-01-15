using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
    public class Maladie
    {
        public Maladie()
        {
            Jour = DateTime.Now.Day;
            Mois = DateTime.Now.Month;
            Annee = DateTime.Now.Year;
        }
        [Key]
        [ScaffoldColumn(false)]
        public int IDMALADIE { get; set; }

        [Display(Name = "Patient")]
        public int? IDPATIENT { get; set; }

        [Display(Name = "Hopital")]
        public int? IDHOPITAL { get; set; }

        [Display(Name = "Centre de Santé")]
        public int? IDCENTRE { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Symptome")]
        public string SYMPTOME { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Désignation")]
        public string DESIGNATION { get; set; }

        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observation")]
        public string OBSERVATION { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Hopital Hopital { get; set; }
        public virtual CentreSante CentreSante { get; set; }

        public int Jour { get; set; }

        public int Mois { get; set; }

        public int Annee { get; set; }
    }
}
