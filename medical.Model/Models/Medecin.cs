using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace medical.Model.Models
{
    public class Medecin:Personnel
    {
        [Display(Name = "Hopital d'Appartenance")]
        public int? IDHOPITAL { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<Examen> Examens { get; set; }
        public virtual ICollection<Rendezvous> Rendezvouss { get; set; }
        public virtual ICollection<ActeMedical> ActeMedicals { get; set; }
        public virtual ICollection<Facturation> Facturations { get; set; }
    }
}
