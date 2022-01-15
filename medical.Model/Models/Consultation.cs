using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Model.Models
{
   
    public class Consultation
    {
        public Consultation()
        {
            DATECONSULTATION = DateTime.Now;
        }

        [Key]
        [ScaffoldColumn(false)]
        public int IDCONSULTATION { get; set; }

        public DateTime? DATECONSULTATION { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Décision")]
        public string DECISION { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Plainte")]
        public string PLAINTE { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Diagnostic")]
        public string DIAGNOSTIC { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Temperature")]
        public double TEMPERATURE { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Pression Artérielle")]
        public double PRESSIONARTERIEL { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Poids")]
        public double POIDS { get; set; }

     
        [Display(Name = "Nom Patient")]
        public int? PATIENTID { get; set; }

       
        [Display(Name = "Nom Médecin")]
        public string Id { get; set; }
    
        public virtual Medecin Medecin { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
