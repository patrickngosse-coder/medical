using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public class Rendezvous
    {
        [Key]
        [ScaffoldColumn(false)]
        public int IDRDV { get; set; }

        
        [Display(Name = "Patient")]
        public int? IDPATIENT { get; set; }

       
        [Display(Name = "Médecin")]
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Motif RDV")]
        public DateTime DATERENDEZVOUS { get; set; }

       
        [StringLength(500, ErrorMessage = "Problème de format texte.", MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Motif RDV")]
        public string NOTE { get; set; }
    
        public virtual Medecin Medecin { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
